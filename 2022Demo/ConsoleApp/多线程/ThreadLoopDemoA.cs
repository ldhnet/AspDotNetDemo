using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{
    public class ThreadLoopDemoA
    {
        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="obj"></param>
        static void CallMethod(object state)
        {
            Console.WriteLine("RunWorkerThread开始工作");
            Console.WriteLine("工作者线程启动成功!");
        }

        public static void Test()
        {
            //工作者线程最大数目，I/O线程的最大数目
            ThreadPool.SetMaxThreads(1000, 1000);
            //启动工作者线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(CallMethod!));
            Console.ReadKey();
        }
    }
}
