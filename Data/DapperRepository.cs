using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test_4._0.Data.Model;

namespace Test_4._0.Data
{
    public class DapperRepository<T> :IDisposable, IDapperRepository<T> where T : class
    {

        private IDbConnection _innerConn = null;
        private IDbTransaction _innerTran = null;

        private IDbConnection _refConn = null;
        private IDbTransaction _refTran = null;

        /// <summary>
        /// 返回仓储类当前连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                if (_refConn != null)
                {
                    return _refConn;
                }
                else
                {
                    return _innerConn;
                }
            }
        }

        /// <summary>
        /// 返回仓储类当前事务
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                if (_refTran != null)
                {
                    return _refTran;
                }
                else
                {
                    return _innerTran;
                }
            }
        }

        public DapperRepository(IConfiguration configuration)
        {
            var sqlConnectionStr = configuration.GetSection("DbConfig:SqlServer:ConnectionString").Value;
            _innerConn = new SqlConnection(sqlConnectionStr); ;
            _innerConn.Open();
            _innerConn.Execute("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }

        public DapperRepository(IDbConnection conn, IDbTransaction trans = null)
        {
            if (conn == null)
            {
                throw new Exception("conn can not be null!");
            }

            if (trans != null)
            {
                if (trans.Connection != conn)
                {
                    throw new Exception("trans'connection must be same as conn!");
                }
            }
            _refConn = conn;
            _refTran = trans;
        }

        public void BeginTrans()
        {
            _innerTran = this.Connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public void Rollback()
        {
            if (Transaction != null)
            {
                this.Transaction.Rollback();
            }
        }

        public void Commit()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Commit();
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">添加数据对象</param>
        /// <returns>返回插入数据的主键</returns>
        public dynamic Add(T entity)
        {
            return this.Connection.Insert<T>(entity, this.Transaction);
        }
        /// <summary>
        /// 添加多组数据
        /// </summary>
        /// <param name="entitys">IEnumerable<T></param>
        /// <returns></returns>
        public List<dynamic> AddBatch(IEnumerable<T> entitys)
        {
            List<dynamic> retVal = new List<dynamic>();
            foreach (T entity in entitys)
            {
                retVal.Add(Add(entity));
            }
            return retVal;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public bool Update(T entity)
        {
            return this.Connection.Update(entity, this.Transaction);
        }
        /// <summary>
        /// 删除数据 根据对象删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public bool Delete(T entity)
        {
            return this.Connection.Delete(entity, this.Transaction);
        }

        /// <summary>
        /// 删除数据  根据主键Id删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(object predicate = null)
        {
            return this.Connection.Delete(predicate, this.Transaction);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T Get(object Id)
        {
            return this.Connection.Get<T>(Id, this.Transaction);
        }
        /// <summary>
        /// 返回所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return this.Connection.GetList<T>(this.Transaction);
        }

        public IEnumerable<T> GetList(string sql, object parameters = null)
        {
            return this.Connection.Query<T>(sql, parameters, this.Transaction);
        }

        public int Execute(string sql, object parameters = null)
        {
            return this.Connection.Execute(sql, parameters, this.Transaction);
        }


        public IEnumerable<T> GetList(IPredicateGroup predGroup, List<ISort> sort)
        {
            IEnumerable<T> list = this.Connection.GetList<T>(predGroup, sort, this.Transaction);
            return list;

        }

        public Tuple<int, IEnumerable<T>> GetPage(IPredicateGroup predicate, int pageindex, int pageSize, List<ISort> sort)
        {
            var multi = this.Connection.GetPage<T>(predicate, sort, pageindex, pageSize, this.Transaction);
            var count = multi.Count();
            var results = multi.ToList();
            return new Tuple<int, IEnumerable<T>>(count, results);
        }

        //public PagedDataTable GetPagedTable(IPredicateGroup predicate, int pageindex, int pageSize, IList<ISort> sort)
        //{
        //    var totalCount = this.Connection.Count<T>(predicate, this.Transaction);

        //    List<T> multi = this.Connection.GetPage<T>(predicate, sort, pageindex, pageSize, this.Transaction).ToList();

        //    PagedDataTable retVal = new PagedDataTable()
        //    {
        //        Data = IITDeductionDataType.Convert<T>(multi),
        //        TotalCount = totalCount,
        //        PageIndex = pageindex,
        //        PageSize = pageSize
        //    };

        //    return retVal;
        //}


        public long Count(IPredicateGroup predicate)
        {
            return this.Connection.Count<T>(predicate, this.Transaction);
        }

        public object ExecuteScalar(string query, object parameters = null)
        {
            return this.Connection.ExecuteScalar(query, parameters, this.Transaction);
        }

        /// <summary>
        /// 多条件组合查询
        /// </summary>
        /// <param name="predGroup"></param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> QueryByPredGroup(IPredicateGroup predGroup, List<ISort> sort)
        {
            IEnumerable<T> list = this.Connection.GetList<T>(predGroup, sort);
            return list;
        }
        /// <summary>
        /// 查询返回List<object>
        /// </summary>
        /// <typeparam name="TAny">自定义传输返回的Obect</typeparam>
        /// <param name="query">querySql</param>
        /// <param name="parameters">querySql参数</param>
        /// <returns></returns>
        public IEnumerable<TAny> Query<TAny>(string query, object parameters = null) where TAny : class
        {
            return Connection.Query<TAny>(query, parameters, Transaction);
        }
        /// <summary>
        /// 通过Linq方式查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        //public T FirstOrDefault(Expression<Func<T, bool>> expression)
        //{
        //    IPredicate ipredicate = expression.ToPredicateGroup();

        //    var List = this.Connection.GetList<T>(ipredicate, null, this.Transaction).FirstOrDefault();
        //    return List;
        //}
        /// <summary>
        /// 通过Linq获取LIST数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        //public IEnumerable<T> GetList(Expression<Func<T, bool>> expression)
        //{
        //    IPredicate ipredicate = expression.ToPredicateGroup();
        //    IEnumerable<T> list = this.Connection.GetList<T>(expression, null, this.Transaction);
        //    return list;
        //}


        public string AddPageQuery(string sql)
        {

            string querySql = "select * from(" + sql

                + @")AS RowConstrainedResult
                             WHERE RowNum >= (@PageIndex * @PageSize + 1)
                                 AND RowNum <= (@PageIndex + 1) * @PageSize
                             ORDER BY RowNum";

            return querySql;
        }



        public void Dispose()
        {
            if (_innerTran != null)
            {
                _innerTran.Dispose();
                _innerTran = null;
            }

            if (_innerConn != null)
            {
                _innerConn.Close();
                _innerConn.Dispose();
                _innerConn = null;
            }
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }

}


