using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程锁
{
    public class AtomicityForLockTest
    {
        private static Object _obj = new object();
        /// <summary>
        /// 原子操作基于Lock实现
        /// </summary>
        public static void AtomicityForLock()
        {
            long result = 0;
            Console.WriteLine("开始计算");
            //10个并发执行
            Parallel.For(0, 10, (i) =>
            {
                //lock锁
                lock (_obj)
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        result++;
                    }
                }
            });
            Console.WriteLine("结束计算");
            Console.WriteLine($"result正确值应为：{10000 * 10}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();

        }
    }
}
