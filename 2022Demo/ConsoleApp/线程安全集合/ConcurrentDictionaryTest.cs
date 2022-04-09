using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.线程安全集合
{
    public class ConcurrentDictionaryTest
    {
        //普通字典 重复值会报错
        private static IDictionary<string, string> Dictionaries { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 字典增加值  
        /// </summary>
        public static void AddDictionaries()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //并发1000个线程写
            Parallel.For(0, 1000, (i) =>
            {
                var key = $"key-{i}";
                var value = $"value-{i}";

                // 不加锁会报错
                // lock (Dictionaries)
                // {
                Dictionaries.Add(key, value);
                // }
            });
            sw.Stop();
            Console.WriteLine("Dictionaries 当前数据量为： {0}", Dictionaries.Count);
            Console.WriteLine("Dictionaries 执行时间为： {0} ms", sw.ElapsedMilliseconds);
        }


        //线程安全字典
        private static IDictionary<string, string> ConcurrentDictionaries { get; set; } = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 线程安全字典添加值
        /// </summary>
        public static void AddConcurrentDictionaries()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //并发1000个线程写
            Parallel.For(0, 1000, (i) =>
            {
                var key = $"key-{i}";
                var value = $"value-{i}";

                // 无须加锁
                ConcurrentDictionaries.Add(key, value);

            });
            sw.Stop();
            Console.WriteLine("ConcurrentDictionaries 当前数据量为： {0}", ConcurrentDictionaries.Count);
            Console.WriteLine("ConcurrentDictionaries 执行时间为： {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
