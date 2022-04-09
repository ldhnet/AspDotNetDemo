using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程锁
{
    public class AtomicityForCASTest
    {
        /// <summary>
        /// 自增CAS实现
        /// Interlocked下原子操作的方法最终都是调用Interlocked.CompareExchange(ref a, b, c)实现的
        /// </summary>
        public static void AtomicityForInterLock()
        {
            long result = 0;
            Console.WriteLine("开始计算");
            Parallel.For(0, 10, (i) =>
            {
                for (int j = 0; j < 10000; j++)
                {
                    //自增
                    Interlocked.Increment(ref result);
                }
            });
            Console.WriteLine($"结束计算");
            Console.WriteLine($"result正确值应为：{10000 * 10}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();
        }

        /// <summary>
        /// 基于CAS原子操作自己写
        /// 基于 Interlocked.CompareExchange
        /// </summary>
        public static void AtomicityForMyCalc()
        {
            long result = 0;
            Console.WriteLine("开始计算");

            Parallel.For(0, 10, (i) =>
            {
                long init = 0;
                long incrementNum = 0;
                for (int j = 0; j < 10000; j++)
                {
                    do
                    {
                        init = result;
                        incrementNum = result + 1;
                        incrementNum = incrementNum > 10000 ? 1 : incrementNum; //自增到10000后初始化成1

                    }
                    //如果result=init,则result的值被incrementNum替换,否则result不变,返回的是result的原始值
                    while (init != Interlocked.CompareExchange(ref result, incrementNum, init));
                    if (incrementNum == 10000)
                    {
                        Console.WriteLine($"自增到达10000啦!值被初始化为1");
                    }
                }
            });
            Console.WriteLine($"结束计算");

            Console.WriteLine($"result正确值应为：{10000}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();

        }

    }
}
