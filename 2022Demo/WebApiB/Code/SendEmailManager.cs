using Lee.Cache;
using Lee.Utility.Config;
using Lee.Utility.Helper;

namespace WebApiB.Code
{
    public class SendEmailManager : ISendEmailManager
    {
        public void SendMailUsingQueue()
        {
            if (SendEmailConfig.MailQueue.Count > 0)
            {
                ScanQueue();
            }
            else
            {
                Thread.Sleep(3000);
            }
        }

        #region 邮件发送队列
 
        public void ScanQueue()
        {
            //SMTP smtp;
            Sys_MailInfo mail;
            while (SendEmailConfig.MailQueue.Count > 0)
            {
                try
                {
                    //从队列中取出  
                    mail = SendEmailConfig.MailQueue.Dequeue();

                    Console.WriteLine(JsonHelper.ToJson(mail));

                    //smtp = new SMTP(mail.To);
                    //smtp.Sendemail(mail);
                }
                catch (Exception ex)
                {
                    //Logger.Error(ex.Message);
                }
                Thread.Sleep(200);
            }
        }

        #endregion
    }

}
