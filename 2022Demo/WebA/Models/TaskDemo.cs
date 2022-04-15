using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Models
{
    public class TaskDemo
    {
        /// <summary>
        /// 一个比较耗时的方法,循环100W次
        /// </summary>
        /// <param name="name"></param>
        private void DoSomething(string name)
        {
            int iResult = 0;
            for (int i = 0; i < 10000000; i++)
            {
                iResult += 1;
            }

            Console.WriteLine($"{name},结果：{iResult}，线程Id:{Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}" + Environment.NewLine);
        
        
        }
        /// <summary>
        /// 停顿10秒
        /// </summary>
        /// <param name="name"></param>
        private void DoSleepSomeTime(string name)
        {
            Thread.Sleep(1000 * 10);
            Console.WriteLine($"{name},线程Id:{Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}" + Environment.NewLine);
             
        }
        private void SayHello()
        {
            Console.WriteLine($"Hello World");
        }
        private static void DoCallback()
        { 
            Console.WriteLine($"计算结束");
        }

        public void GetString()
        { 
            Action say = SayHello; 
            say.Invoke();
             
            Console.WriteLine($"**************计算开始，线程Id:{Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}*****************");

            for (int i = 1; i <= 5; i++)
            {
                DoSleepSomeTime($"第{i}个测试");
            }
            Console.WriteLine($"**************计算结束,线程Id:{Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}*****************");
             
            //asyncResult = action.BeginInvoke("begin", callback, null);
        }

        public void GetStringAsync()
        {
            IAsyncResult asyncResult = null;

            AsyncCallback callback = ar => { DoCallback(); };

            AsyncCallback callback2 = ar2 =>
            {

                Console.WriteLine(object.ReferenceEquals(ar2, asyncResult));
                Console.WriteLine("计算结束。。。");
                Console.WriteLine($"{ar2.AsyncState}");

            };

            Action say = SayHello;
            say(); 

            Action<string> action = DoSleepSomeTime;
            Console.WriteLine($"**************计算开始，线程Id:{Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}*****************");

            for (int i = 1; i <= 5; i++)
            { 
                action.Invoke($"第{i}个测试"); 
            } 
            Console.WriteLine($"**************计算结束,线程Id:{Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}*****************");
        
            //asyncResult = action.BeginInvoke("begin", callback, null);
        }


    }
}

