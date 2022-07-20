using Lee.Utility.Config;
using Lee.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WebApiB.ChannelCode
{
    public  class ChannelContext
    {
        private  Channel<Sys_MailInfo> channel { get; set; }
        public ChannelContext()
        {
            channel = Channel.CreateUnbounded<Sys_MailInfo>();
        }
         
        public  void AddMailToChannel(Sys_MailInfo sys_MailInfo)
        {  
            Task.Run(async () =>
            {
                Console.WriteLine($"WriteAsync ==={DateTime.Now}，当前线程== {Thread.CurrentThread.ManagedThreadId} ========== {Task.CurrentId}");
                await Task.Delay(TimeSpan.FromMilliseconds(200));
                await channel.Writer.WriteAsync(sys_MailInfo);// 生产者写入消息
                channel.Writer.Complete(); //生产者也可以明确告知消费者不会发送任何消息了
            });
        }

        public void ScanEmailChannel()
        { 

            Task.Run(async () =>
            {
                Console.WriteLine($"Reader ===当前线程============ {Thread.CurrentThread.ManagedThreadId} ========== {Task.CurrentId}");
                await foreach (var item in channel.Reader.ReadAllAsync())//async stream,在没有被生产者明确Complete的情况下，这里会一致阻塞下去
                {
                    Console.WriteLine($"ReadAllAsync===={JsonHelper.ToJson(item)}");
                } 
            }); 
        }
    }
} 
