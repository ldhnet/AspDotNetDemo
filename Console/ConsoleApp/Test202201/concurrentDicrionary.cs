using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test202201
{
    public class concurrentDicrionary
    {
        const string Item = "";
        public static string CurrentItem;
        public static void DicrionaryTest()
        {
            var concurrentDicrionary = new ConcurrentDictionary<int, string>();
            var dictionary = new Dictionary<int, string>();

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                lock (dictionary)
                {
                    dictionary[i] = Item;
                }
            }
            sw.Stop();
            Console.WriteLine("写dictionary的时间" + sw.Elapsed);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                concurrentDicrionary[i] = Item;
            }
            sw.Stop();
            Console.WriteLine("写并发集合concurrentDicrionary的时间：" + sw.Elapsed);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                lock (dictionary)
                {
                    CurrentItem = dictionary[i];
                }
            }
            sw.Stop();
            Console.WriteLine("读dictionary的时间" + sw.Elapsed);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                CurrentItem = concurrentDicrionary[i];
            }
            sw.Stop();
            Console.WriteLine("读并发集合concurrentDicrionary的时间：" + sw.Elapsed);

            Console.ReadKey();
        }
    }
}
