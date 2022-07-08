using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ChannelTest
    {
        public Channel<int> channel { get; set; }
        public ChannelTest()
        {
            channel = Channel.CreateUnbounded<int>();
        }
         
        public void testMain()
        {  
            Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"WriteAsync ===第{i}次，当前线程== {Thread.CurrentThread.ManagedThreadId} ========== {Task.CurrentId}");
                    await Task.Delay(TimeSpan.FromMilliseconds(200));
                    await channel.Writer.WriteAsync(i);// 生产者写入消息
                    if (i > 5)
                    {
                        channel.Writer.Complete(); //生产者也可以明确告知消费者不会发送任何消息了
                    }
                }

                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine($"第二次循环 WriteAsync ===第{i}次，当前线程== {Thread.CurrentThread.ManagedThreadId} === {Task.CurrentId}");
                //    await Task.Delay(TimeSpan.FromMilliseconds(200));
                //    await channel.Writer.WriteAsync(i);// 生产者写入消息 
                //    if (i > 9)
                //    {
                //        channel.Writer.Complete(); //生产者也可以明确告知消费者不会发送任何消息了
                //    }
                //} 
     
            });

            Console.WriteLine($"WriteAsync ==========================结束============================");

            Task.Run(async () =>
            {
                Console.WriteLine($"Reader ===当前线程============ {Thread.CurrentThread.ManagedThreadId} ========== {Task.CurrentId}");
                await foreach (var item in channel.Reader.ReadAllAsync())//async stream,在没有被生产者明确Complete的情况下，这里会一致阻塞下去
                {
                    Console.WriteLine($"ReadAllAsync===={item}");
                }

            });

            Console.ReadKey();
        } 
    }
} 
