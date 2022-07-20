using Lee.Cache;
using Lee.Utility.Config;
using Lee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiB.Code
{
    public static class MailExtensions
    {
        //private static readonly ILogger Logger = GlobalConfig.ServiceProvider.GetRequiredService<ILogger>();

        private static readonly string TesterEmailReceiver = "574427343@qq.com;574427343@qq.com";
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
            var aaaa= TesterEmailReceiver;
            try
            {
                lock (MailToQueuePadlock)
                {
#if DEBUG
                    if (!TesterEmailReceiver.IsNullOrEmpty())
                    {
                        if (TesterEmailReceiver.Contains(";"))
                        {
                            var receivers = TesterEmailReceiver.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            mailInfo.To = receivers[0];
                            if (receivers.Count > 1)
                            {
                                mailInfo.CCList = receivers.Skip(1).ToArray();
                            }
                        }
                        else
                        {
                            mailInfo.To = TesterEmailReceiver;
                        }
                    } 
#endif
                    if (mailInfo.To.IsNullOrEmpty() || !mailInfo.To.IsEmail()) return;

          
                    SendEmailConfig.MailQueue.Enqueue(mailInfo);
                     

                    Console.WriteLine($"AddMailToQueue==={Thread.CurrentThread.Name}=={Thread.CurrentThread.ManagedThreadId}====={DateTime.Now}=={SendEmailConfig.MailQueue.Count}");
                     
                      
                }
            }
            catch (Exception ex)
            { 
                //Logger.LogError("Error occured when update MailQueue cache: {0}", ex.Message);
            }
        }



        public static void AddMailToChannel(this Sys_MailInfo sys_MailInfo)
        {
            Task.Run(async () =>
            {
                Console.WriteLine($"WriteAsync ==={DateTime.Now}，当前线程== {Thread.CurrentThread.ManagedThreadId} ========== {Task.CurrentId}");
                await Task.Delay(TimeSpan.FromMilliseconds(200));
                await SendEmailConfig.channel.Writer.WriteAsync(sys_MailInfo);// 生产者写入消息
                SendEmailConfig.channel.Writer.Complete(); //生产者也可以明确告知消费者不会发送任何消息了
            });
        }
    }
}
