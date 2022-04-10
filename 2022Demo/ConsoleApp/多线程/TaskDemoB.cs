using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{ 
    public class TaskDemoB
    {
        public static void TaskMain()
        {
            for (int i = 0; i < 2000; i++)
            {
                Task.Run(async () =>
                {
                    await GetString();
                });
            }

            Console.ReadLine();
        }

        public static int counter = 0;

        static async Task<string> GetString()
        {
            var httpClient = new HttpClient();

            var str = await httpClient.GetStringAsync("http://cnblogs.com");

            Console.WriteLine($"counter={++counter},时间:{DateTime.Now}, 线程：{Thread.CurrentThread.ManagedThreadId},str.length={str.Length}");

            while (true) { }

            return str;
        }
    }
}
