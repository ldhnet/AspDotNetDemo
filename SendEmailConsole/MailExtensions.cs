using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailConsole
{
    public static class MailExtensions
    { 
        private static readonly string TesterEmailReceiver = "";
        private static readonly string ceoEmail = "";
        private static readonly string assistantEmail = "";

        /// <summary>
        /// 添加邮件队列静态锁
        /// </summary>
        private static readonly object MailToQueuePadlock = new object();

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailInfo"></param>
        public static void AddMailToQueue(this Sys_MailInfo mailInfo)
        {
            try
            {
                var queue = new Queue<Sys_MailInfo>();

                lock (MailToQueuePadlock)
                { 
                    if (string.IsNullOrEmpty(mailInfo.To)) return;
                
                    queue.Enqueue(mailInfo);
                }
                //var smtp = new SMTP("574427343@qq.com");
                while (queue.Count != 0)
                {
                    var mail = queue.Dequeue();

                    var smtp = new SMTP(mail.To);
                    smtp.Sendemail(mail);
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        /// <summary>
        /// CC 列表
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="ccList"></param>
        public static void ConfigCCList(this MailMessage mailMessage, string[] ccList)
        {
            if (ccList != null && ccList.Length > 0)
            {
                foreach (var address in ccList)
                {
                    mailMessage.CC.Add(address);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="attachments"></param>
        public static void ConfigAttachments(this MailMessage mailMessage, string[] attachments)
        {
            if (attachments != null)
            {
                foreach (var fileName in attachments.Where(File.Exists))
                {
                    mailMessage.Attachments.Add(new Attachment(fileName));
                }
            }
        }

        private static void CEOMall(Sys_MailInfo mailInfo)
        {
            if (mailInfo.To.ToLower() == ceoEmail)
            {
                mailInfo.CCList = new string[] { assistantEmail };
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <param name="bodyinfo"></param>
        /// <param name="image"></param>
        public static void ConfigAlternateViews(this MailMessage mailMessage, string bodyinfo, AttachedImage image)
        {
            mailMessage.SubjectEncoding = Encoding.GetEncoding("gb2312");

            var htmlBody = AlternateView.CreateAlternateViewFromString(bodyinfo, null, "text/html");

            var lrImage = new LinkedResource(image.FileName, image.MediaType) { ContentId = image.ContentId };
            htmlBody.LinkedResources.Add(lrImage);
            mailMessage.AlternateViews.Add(htmlBody);
        }
    }
}
