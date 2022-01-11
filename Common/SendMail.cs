using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_4._0.Common
{
    public interface ISendMail
    {
        Task<string> SendMailAsync(string toAddress, string ccAddress, string subject, string body);
        Task<bool> IsMail(string email);

    }
    public class SendMail : ISendMail
    {
        private readonly SendMailConfig _sendMailConfig;
       
        public SendMail(SendMailConfig mailConfig)
        {
            _sendMailConfig = mailConfig;
        }
        public async Task<string> SendMailAsync(string toAddress, string ccAddress, string subject, string body)
        {
            if (!_sendMailConfig.IsOpen)
            {
                return "0";
            }
            var isEmail = IsMail(toAddress).Result;
            if (!isEmail || string.IsNullOrEmpty(toAddress))
            {
                return "0";
            }
            try
            {
                MailMessage msg = new MailMessage();
                //邮件主题
                msg.Subject = subject;
                //邮件正文
                msg.Body = body;
                //正文编码
                msg.BodyEncoding = Encoding.UTF8;
                //优先级
                msg.Priority = MailPriority.Normal;
                //发件者邮箱地址
                MailAddress fromAddr = new MailAddress(_sendMailConfig.SendAccount);
                msg.From = fromAddr;
                //收件人收箱地址
                msg.To.Add(toAddress);
                if (!string.IsNullOrEmpty(ccAddress)) msg.CC.Add(ccAddress);
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的.企业邮箱跟个人不一致
                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(_sendMailConfig.SendAccount, _sendMailConfig.SendPwd);
                //启用ssl,也就是安全发送
                client.EnableSsl = true;
                //发送邮件
                await client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                //_log.Error("error SendMailAsync 错误信息为：" + ex);
            }
            return body;

        }


        public async Task<bool> IsMail(string email)
        {
            var isReturn = false;
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            email = email.Trim();
            string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            isReturn = Regex.IsMatch(email, pattern);
            return isReturn;
        }
    }
}
