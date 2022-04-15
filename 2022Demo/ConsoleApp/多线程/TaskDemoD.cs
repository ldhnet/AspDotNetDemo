using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{
    public class TaskDemoD
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
 
        public static void DoCallback()
        { 
            Console.WriteLine($"计算结束");
        }

        public void GetString()
        {
            IAsyncResult asyncResult = null;

            AsyncCallback callback = ar => { DoCallback(); };

            AsyncCallback callback2 = ar2 =>
            {

                Console.WriteLine(object.ReferenceEquals(ar2, asyncResult));
                Console.WriteLine("计算结束。。。");
                Console.WriteLine($"{ar2.AsyncState}");
            }; 

            Action<string> action = DoSomething;

            action.Invoke("第一个测试");




            //asyncResult = action.BeginInvoke("begin", callback, null);
        }


    }
}

