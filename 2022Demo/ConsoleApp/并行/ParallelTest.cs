using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.并行
{
    public class ParallelTest
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


        public static void Test1()
        {
            //并行编程 
            Console.WriteLine($"并行编程开始，主线程Id:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("【示例1】");
            //示例1：
            //一次性执行1个或多个线程，效果类似：Task WaitAll，只不过Parallel的主线程也参与了计算
            Parallel.Invoke(
                () => { DoSomething("并行1-1"); },
                () => { DoSomething("并行1-2"); },
                () => { DoSomething("并行1-3"); },
                () => { DoSomething("并行1-4"); },
                () => { DoSomething("并行1-5"); });
            Console.WriteLine("*************并行结束************");
            Console.ReadLine();
        }

        public static void Test2()
        {
            //并行编程 
            Console.WriteLine($"并行编程开始，主线程Id:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("【示例2】");
            //示例2：
            //定义要执行的线程数量
            Parallel.For(0, 5, t =>
            {
                int a = t;
                DoSomething($"并行2-{a}");

            });
            Console.WriteLine("*************并行结束************");
            Console.ReadLine();
        }

        public static void Test3()
        {
            //并行编程 
            Console.WriteLine($"并行编程开始，主线程Id:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("【示例3】");
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 3//执行线程的最大并发数量,执行完成一个，就接着开启一个
            };

            //遍历集合，根据集合数量执行线程数量
            Parallel.ForEach(new List<string> { "a", "b", "c", "d", "e", "f", "g" }, options, (t, status) =>

            {
                //status.Break();//这一次结束。
                //status.Stop();//整个Parallel结束掉，Break和Stop不可以共存
                DoSomething($"并行4-{t}");
            });
        }
    }
}
