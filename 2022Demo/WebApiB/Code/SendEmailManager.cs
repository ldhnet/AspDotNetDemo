using Lee.Cache;
using Lee.Utility.Config;
using Lee.Utility.Helper;

namespace WebApiB.Code
{
    public class SendEmailManager : ISendEmailManager
    {
        public void SendMailUsingQueue()
        {
            Console.WriteLine($"SendEmailConfig.MailQueue.Count==={Thread.CurrentThread.Name}=={Thread.CurrentThread.ManagedThreadId}====={DateTime.Now}=={SendEmailConfig.MailQueue.Count}");
            if (SendEmailConfig.MailQueue.Count > 0)
            {
                ScanQueue();
            }
            else
            {
                Thread.Sleep(3000);
            }
        }

        public void SendMailUsingChannel()
        {
           var a=  SendEmailConfig.channel.Reader.CanCount;
            var b = SendEmailConfig.channel.Reader.Count;
            var c = SendEmailConfig.channel.Reader.CanPeek;
             

            Console.WriteLine($"SendMailUsingChannel========{Thread.CurrentThread.Name}=={Thread.CurrentThread.ManagedThreadId}====={DateTime.Now}======channel数量==={b}");
            Task.Run(async () =>
            {
                Console.WriteLine($"Reader ===当前线程============ {Thread.CurrentThread.ManagedThreadId} ========== {Task.CurrentId}");
                await foreach (var item in SendEmailConfig.channel.Reader.ReadAllAsync())//async stream,在没有被生产者明确Complete的情况下，这里会一致阻塞下去
                {
                    Console.WriteLine($"ReadAllAsync===={JsonHelper.ToJson(item)}");
                }
            });
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
