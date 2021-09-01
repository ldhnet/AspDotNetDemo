using System;
using System.ComponentModel;

namespace SendEmailConsole
{
    class Program
    {
         
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SendMailUsingQueue(); 

            var emailhelper = new EmailHelper();


            var smtp = new SMTP("574427343@qq.com");

            var resultE = smtp.Sendemail("请假单通知(系统邮件)", ("您好 {XXXXX}"));


            //var mailInfo = new Sys_MailInfo
            //{
            //    To = "574427343@qq.com",
            //    Subject = "LDH{邮件}通知(系统邮件)",
            //    Body = "您好 {测试测试测试得分邮件}"
            //};

            //for (int i = 0; i < 5; i++)
            //{
            //    mailInfo.AddMailToQueue();
            //}


        }

        private static void SendMailUsingQueue()
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, e) =>
            {
                int sum = 0;
                for (int i = 0; i <= 100; i++)
                {
                    sum += i;
                    Console.WriteLine($"BackgroundWorker Is:{sum}");
                }
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                worker.RunWorkerAsync();
            };

            worker.RunWorkerAsync();
        }
    }
}
