﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{
    public class TaskDemo
    {
        /// <summary>
        /// 一个比较耗时的方法,循环1000W次
        /// </summary>
        /// <param name="name"></param>
        public static void DoSomething(string name)
        {
            int iResult = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                iResult += i;
            }

            Console.WriteLine($"{name},线程Id:{Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}" + Environment.NewLine);
        }

        public static void Test()
        {
            //线程容器
            List<Task> taskList = new List<Task>();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("************任务开始**************");

            //启动5个线程
            for (int i = 0; i < 5; i++)
            {
                string name = $"Task:{i}";
                Task task = Task.Factory.StartNew(() =>
                {
                    DoSomething(name);
                });

                taskList.Add(task);
            }

            //回调形式的，任意一个完成后执行的后续动作
            Task any = Task.Factory.ContinueWhenAny(taskList.ToArray(), task =>
            {
                Console.WriteLine($"ContinueWhenAny,当前线程Id:{Thread.CurrentThread.ManagedThreadId},一个线程执行完的回调，继续执行后面动作，{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}" + Environment.NewLine);
            });

            //回调形式的，全部任务完成后执行的后续动作
            Task all = Task.Factory.ContinueWhenAll(taskList.ToArray(), tasks =>
            {
                Console.WriteLine($"ContinueWhenAll,当前线程Id:{Thread.CurrentThread.ManagedThreadId},全部线程执行完的回调,线程数：{tasks.Length},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}" + Environment.NewLine);
            });

            //把两个回调也放到容器里面，包含回调一起等待
            taskList.Add(any);
            taskList.Add(all);

            //WaitAny:线程等待，当前线程等待其中一个线程完成
            Task.WaitAny(taskList.ToArray());
            Console.WriteLine($"WaitAny,当前线程Id:{Thread.CurrentThread.ManagedThreadId},其中一个完成已完成,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}" + Environment.NewLine);

            //WaitAll:线程等待，当前线程等待所有线程的完成
            Task.WaitAll(taskList.ToArray());
            Console.WriteLine($"WaitAll，当前线程Id:{Thread.CurrentThread.ManagedThreadId},全部线程完成，{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}" + Environment.NewLine);

            Console.WriteLine($"************任务结束**************耗时：{watch.ElapsedMilliseconds}ms,{Thread.CurrentThread.ManagedThreadId},{DateTime.Now}");
        }

        /// <summary>
        /// 取消异步线程执行
        /// </summary>
        public void CancelTask()
        {
            Console.WriteLine($"******Start,线程Id:{Thread.CurrentThread.ManagedThreadId}**********");
            CancellationTokenSource source = new CancellationTokenSource();
            //注册任务取消的事件
            source.Token.Register(() =>
            {
                Console.WriteLine($"线程Id:{Thread.CurrentThread.ManagedThreadId},任务被取消后执行xx操做！");
            });

            int index = 0;
            //开启一个task执行任务
            Task task1 = new Task(() =>
            {
                while (!source.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"第{++index}次执行,线程Id:{Thread.CurrentThread.ManagedThreadId}，线程运行中...");
                }
            });
            task1.Start();
            //延时取消，效果等同于Thread.Sleep(5000);source.Cancel();
            source.CancelAfter(5000);

            Console.WriteLine($"******End,线程Id:{Thread.CurrentThread.ManagedThreadId}**********");
            Console.ReadKey();
        }

        /// <summary>
        /// 取消async/await 异步操作  取消失败了
        /// </summary>
        /// <returns></returns> 
        public async Task GetTaskRun()
        {

            Console.WriteLine($"******Start,线程Id:{Thread.CurrentThread.ManagedThreadId}**********");

            int index = 0;

            for (int i = 0; i < 10; i++)
            {
                await Task.Run(() =>
                {
                    Task.Delay(1000 * 10);
                    Console.WriteLine($"第{++index}次执行,线程Id:{Thread.CurrentThread.ManagedThreadId}，线程运行中...");
                }); 
            }
            //不会阻塞主线程
            await Task.Run(() =>
            {
                Task.Delay(1000 * 10);
                Console.WriteLine($"******Task.Run 测试-1， 线程Id:{Thread.CurrentThread.ManagedThreadId}**********");
            });

            //会阻塞主线程
            Task.Run(() =>
            {
                Console.WriteLine($"******Task.Run 测试-2， 线程Id:{Thread.CurrentThread.ManagedThreadId}**********");
            }).GetAwaiter().GetResult();

            //会阻塞主线程
            //await Task.Run(async () =>
            //{
            //    await Task.Delay(1000 * 10);
            //    Console.WriteLine($"******Task.Run 测试-3， 线程Id:{Thread.CurrentThread.ManagedThreadId}**********");
            //});

            Console.WriteLine($"******End,线程Id:{Thread.CurrentThread.ManagedThreadId}**********");
        }
    }
}
