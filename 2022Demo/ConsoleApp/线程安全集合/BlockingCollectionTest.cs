using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.线程安全集合
{
    public class BlockingCollectionTest
    {
        /// <summary>
        /// 线程安全集合用法
        /// </summary>
        public static void BC()
        {
            //线程安全集合
            using (BlockingCollection<int> blocking = new BlockingCollection<int>())
            {
                int NUMITEMS = 10000;

                for (int i = 1; i < NUMITEMS; i++)
                {
                    blocking.Add(i);
                }
                //完成添加
                blocking.CompleteAdding();

                int outerSum = 0;

                // 定义一个委托方法取出集合元素
                Action action = () =>
                {
                    int localItem;
                    int localSum = 0;

                    //取出并删除元素，先进先出
                    while (blocking.TryTake(out localItem))
                    {
                        localSum += localItem;
                    }
                    //两数相加替换第一个值
                    Interlocked.Add(ref outerSum, localSum);
                };

                //并行3个线程执行，多个线程同时取集合的数据
                Parallel.Invoke(action, action, action);

                Console.WriteLine($"0+...{NUMITEMS - 1} = {((NUMITEMS * (NUMITEMS - 1)) / 2)},输出结果：{outerSum}");
                //此集合是否已标记为已完成添加且为空
                Console.WriteLine($"线程安全集合.IsCompleted={blocking.IsCompleted}");
            }
        }

        /// <summary>
        /// 限制集合长度
        /// </summary>
        public static void BCLimtLength()
        {
            //限制集合长度为5个，后面进的会阻塞等集合少于5个再进来
            BlockingCollection<int> blocking = new BlockingCollection<int>(5);

            var task1 = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    blocking.Add(i);
                    Console.WriteLine($"集合添加:({i})");
                }

                blocking.CompleteAdding();
                Console.WriteLine("完成添加");
            });

            // 延迟500ms执行等待先生产数据
            var task2 = Task.Delay(500).ContinueWith((t) =>
            {
                while (!blocking.IsCompleted)
                {
                    var n = 0;
                    if (blocking.TryTake(out n))
                    {
                        Console.WriteLine($"取出:({n})");
                    }
                }

                Console.WriteLine("IsCompleted = true");
            });

            Task.WaitAll(task1, task2);
        }


        /// <summary>
        /// 线程安全集合，先进后出
        /// </summary>
        public static void BCStack()
        {
            //线程安全集合，参数表明栈标识，队列长度为5
            BlockingCollection<int> blocking = new BlockingCollection<int>(new ConcurrentStack<int>(), 5);

            var task1 = Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    blocking.Add(i);
                    Console.WriteLine($"集合添加:({i})");
                }

                blocking.CompleteAdding();
                Console.WriteLine("完成添加");
            });

            // 等待先生产数据
            var task2 = Task.Delay(500).ContinueWith((t) =>
            {
                while (!blocking.IsCompleted)
                {
                    var n = 0;
                    if (blocking.TryTake(out n))
                    {
                        Console.WriteLine($"取出:({n})");
                    }
                }

                Console.WriteLine("IsCompleted = true");
            });

            Task.WaitAll(task1, task2);
        }
    }
}
