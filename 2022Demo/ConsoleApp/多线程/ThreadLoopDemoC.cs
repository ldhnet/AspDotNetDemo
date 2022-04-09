using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{
    public class ThreadLoopDemoC
    {
        /// <summary>
        /// 执行  --线程池等待
        /// </summary>
        public static void Test()
        {
            //用来控制线程等待,false默认为关闭状态
            ManualResetEvent mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(p =>
            {

                Console.WriteLine("线程1开始");
                Thread.Sleep(1000);
                Console.WriteLine($"线程1结束，带参数,{Thread.CurrentThread.ManagedThreadId}");
                //通知线程，修改信号为true
                mre.Set();
            });

            //阻止当前线程，直到等到信号为true在继续执行
            mre.WaitOne();

            //关闭线程，相当于设置成false
            mre.Reset();
            Console.WriteLine("信号被关闭了");

            ThreadPool.QueueUserWorkItem(p =>
            {
                Console.WriteLine("线程2开始");
                Thread.Sleep(2000);
                Console.WriteLine("线程2结束");
                mre.Set();

            });

            mre.WaitOne();
            Console.WriteLine("主线程结束");

        }
    }
}
