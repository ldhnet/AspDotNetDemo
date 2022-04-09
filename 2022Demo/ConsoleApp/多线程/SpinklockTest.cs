using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{
    public class SpinklockTest
    {
        //创建自旋锁
        private static SpinLock spin = new SpinLock();
        public static void Spinklock()
        {
            Action action = () =>
            {
                bool lockTaken = false;
                try
                {
                    //申请获取锁
                    spin.Enter(ref lockTaken);
                    //临界区
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"当前线程{Thread.CurrentThread.ManagedThreadId.ToString()},输出:1");
                    }
                }
                finally
                {
                    //工作完毕，或者产生异常时，检测一下当前线程是否占有锁，如果有了锁释放它
                    //避免出行死锁
                    if (lockTaken)
                    {
                        spin.Exit();
                    }
                }
            };
            Action action2 = () =>
            {
                bool lockTaken = false;
                try
                {
                    //申请获取锁
                    spin.Enter(ref lockTaken);
                    //临界区
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"当前线程{Thread.CurrentThread.ManagedThreadId.ToString()},输出:2");
                    }

                }
                finally
                {
                    //工作完毕，或者产生异常时，检测一下当前线程是否占有锁，如果有了锁释放它
                    //避免出行死锁
                    if (lockTaken)
                    {
                        spin.Exit();
                    }
                }

            };
            //并行执行2个action
            Parallel.Invoke(action, action2);

        }
    }
}
