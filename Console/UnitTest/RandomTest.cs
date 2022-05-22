using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class RandomTest
    {
        /// <summary>
        /// 生成一个10位随机数
        /// 设定了一定的复杂性，保证单线程下随机数不重复
        /// </summary>
        /// <param name="random">Random.</param>
        /// <returns>随机数.</returns>
        private static string GenerateRandomStr(Random random)
        {
            string source = "ABCDEFGHIKLMNOPQRTUVWXYZabcdefghiklmnopqrtuvwxyz";
            int length = 10;
            var list = Enumerable.Repeat(source, length)
                 .Select(s => s[random.Next(s.Length)]).ToArray();
            return new string(list);
        }
        /// <summary>
        /// 单线程基本可以保证唯一性
        /// </summary>
        public static void Good_Random_In_SingleThread()
        {
            //正确做法应当将 Random构建防止循环外。
            //Random创建间隔时间极短的情况下，随机算法序列会基本一致，倒是随机性也是一致的
            var r = new Random();
            ConcurrentBag<string> list = new ConcurrentBag<string>();
            for (int i = 0; i < 20000; i++)
            {
                var val = GenerateRandomStr(r);
                list.Add(val);
            }

            Console.WriteLine($"单线程下重复数据有：{20000 - list.Distinct().Count()}");
        }

        /// <summary>
        /// 多线程下的Random构建。
        /// Bad案例，Random非线程安全
        /// 多线程高并发情况下，会出现概率重复
        /// </summary>
        public static void Bad_Random_In_MultThreads()
        {
            var r = new Random(unchecked((int)DateTime.Now.Ticks));
            ConcurrentBag<string> list = new ConcurrentBag<string>();

            var t1 = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    var val = GenerateRandomStr(r);
                    list.Add(val);
                }
            });

            var t2 = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    var val = GenerateRandomStr(r);
                    list.Add(val);
                }
            });

            Task.WaitAll(t1, t2);

            Console.WriteLine($"线程1和线程2的重复数据有：{20000 - list.Distinct().Count()}");
        }
    }
}
