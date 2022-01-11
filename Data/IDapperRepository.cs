using DapperExtensions;
using DapperExtensions.Predicate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Test_4._0.Data
{
    public interface IDapperRepository<T> where T : class
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        dynamic Add(T entity);

        List<dynamic> AddBatch(IEnumerable<T> entitys);

        bool Update(T entity);

        bool Delete(T entity);

        bool Delete(object Id);

        T Get(object Id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetList(string sql, object parameters = null);

        int Execute(string sql, object parameters = null);

        long Count(IPredicateGroup predicate);
        object ExecuteScalar(string query, object parameters = null);


        T FirstOrDefault(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetList(IPredicateGroup predGroup, List<ISort> sort);

        IEnumerable<TAny> Query<TAny>(string query, object parameters = null) where TAny : class;
    }
}
