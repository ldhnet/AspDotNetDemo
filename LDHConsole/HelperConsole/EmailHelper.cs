using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Linq;

namespace HelperConsole
{
    /// <summary>
    /// 邮件发送类
    /// </summary>
    public class SMTP
    {
        private readonly string _sender = "574427343@qq.com"; //发送方邮箱
        private readonly string _pwd = "xckxdqremjpkbebe"; //发送方密码
        private readonly string _to; //接收方邮箱账号
        private readonly string _smtpServer = "smtp.qq.com";//服务器

        private readonly string _port = "Port";
        private readonly string _enableSsl = "false";
         

        /// <summary>
        /// 邮件发送初始化
        /// </summary>
        /// <param name="to"></param>
        public SMTP(string to) //第1个参数：接收方邮箱账号
        {
            _to = to.Trim();
        }

        private SmtpClient CreateSmtpClient(string host, string userName, string password)
        {
            var smtpClient = new SmtpClient
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = host,
                Credentials = new NetworkCredential(userName, password),
            };
            if (_to.ToLower().TrimEnd().EndsWith("qq.com")) return smtpClient;

            smtpClient.EnableSsl = Convert.ToBoolean(_enableSsl);
            if (!string.IsNullOrEmpty(_port))
                smtpClient.Port = Convert.ToInt32(_port);

            return smtpClient;
        }

        private MailMessage ConstructMailMessage(string subject, string bodyinfo)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_sender), //发件人，发件人名 
                SubjectEncoding = Encoding.UTF8,
                Subject = subject, //主题
                Body = bodyinfo, //内容
                BodyEncoding = Encoding.UTF8, //正文编码
                IsBodyHtml = true, //设置为HTML格式
                Priority = MailPriority.Normal //优先级
            };

            //收件人 
            mailMessage.To.Add(_to);
           
            return mailMessage;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Sendemail(MailMessage message)
        {
            bool flag;

            try
            {
                var smtpClient = CreateSmtpClient(_smtpServer, _sender, _pwd);
                //var smtpClient = SmtpClientInstance.Instance.Client;

                if (_to.ToLower().TrimEnd().EndsWith("qq.com"))
                {
                    smtpClient.EnableSsl = false;
                }
                else
                {
                    smtpClient.EnableSsl = Convert.ToBoolean(_enableSsl);
                    if (!string.IsNullOrEmpty(_port))
                        smtpClient.Port = Convert.ToInt32(_port);
                }

                smtpClient.Send(message); 
                flag = true;
            }
            catch (Exception ex)
            { 
                flag = false;
            } 
            return flag;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="bodyinfo"></param>
        /// <returns></returns>
        public bool Sendemail(string subject, string bodyinfo)
        {
            var mailMessage = ConstructMailMessage(subject, bodyinfo);
            return Sendemail(mailMessage);
        }

        /// <summary>
        /// 发送带CC列表的邮件
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="bodyinfo"></param>
        /// <param name="ccList"></param>
        /// <returns></returns>
        public bool Sendemail(string subject, string bodyinfo, string[] ccList)
        {
            var mailMessage = ConstructMailMessage(subject, bodyinfo);

            //CC 列表
            mailMessage.ConfigCCList(ccList);
            return Sendemail(mailMessage);
        }

        /// <summary>
        /// 发送带CC列表+带附件的邮件
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="bodyinfo"></param>
        /// <param name="ccList"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public bool Sendemail(string subject, string bodyinfo, string[] ccList, string[] attachments)
        {
            var mailMessage = ConstructMailMessage(subject, bodyinfo);
            mailMessage.ConfigCCList(ccList);
            mailMessage.ConfigAttachments(attachments);

            return Sendemail(mailMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="bodyinfo"></param>
        /// <param name="image"></param>
        /// <param name="ccList"></param>
        /// <returns></returns>
        public bool Sendemail(string subject, string bodyinfo, string[] ccList, AttachedImage image)
        {
            // if (string.IsNullOrEmpty(image)) return Sendemail(subject, bodyinfo);

            var mailMessage = ConstructMailMessage(subject, bodyinfo);

            mailMessage.ConfigCCList(ccList);

            mailMessage.ConfigAlternateViews(bodyinfo, image);

            return Sendemail(mailMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailInfo"></param>
        /// <returns></returns>
        public bool Sendemail(Sys_MailInfo mailInfo)
        {
            var mailMessage = ConstructMailMessage(mailInfo.Subject, mailInfo.Body);

            if (mailInfo.AttachedImage != null)
            {
                mailMessage.ConfigAlternateViews(mailInfo.Body, mailInfo.AttachedImage);
            }
            if (mailInfo.CCList != null)
            {
                mailMessage.ConfigCCList(mailInfo.CCList);
            }
            if (mailInfo.AttachmentFiles != null)
            {
                mailMessage.ConfigAttachments(mailInfo.AttachmentFiles);
            }

            return Sendemail(mailMessage);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Sys_MailInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] CCList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] AttachmentFiles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AttachedImage AttachedImage { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AttachedImage
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MediaType { get; set; } = "image/jpg";

        /// <summary>
        /// 
        /// </summary>
        public string ContentId { get; set; }
    }
    public class EmailHelper
    {



        /// <summary>
        /// 发送邮件,成功返回true,否则false
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="body">内容</param>
        /// <param name="title">标题</param>
        /// <param name="whichEmail">是否join</param>
        /// <param name="path">附件</param>
        /// <param name="Fname">姓名</param>
        /// <returns>结果</returns>
        public bool SentMailHXD(string to, string body, string title, string whichEmail, string path, string Fname)
        {
            bool retrunBool = false;
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string strFromEmail = "574427343@qq.com";//你的邮箱
            string strEmailPassword = "";//QQPOP3/SMTP服务码 xckxdqremjpkbebe
            try
            {
                mail.From = new MailAddress("" + Fname + "<" + strFromEmail + ">");
                mail.To.Add(new MailAddress(to));
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                mail.Body = body;
                mail.Subject = title;
                smtp.Host = "smtp.qq.com";
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(strFromEmail, strEmailPassword);
                //发送邮件
                smtp.Send(mail);   //同步发送
                retrunBool = true;
            }
            catch (Exception ex)
            {
                retrunBool = false;
            }
            // smtp.SendAsync(mail, mail.To); //异步发送 （异步发送时页面上要加上Async="true" ）
            return retrunBool;
        }
    }
}