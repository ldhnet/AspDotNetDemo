using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{
    public class ThreadLoopDemoB
    {
        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="obj"></param>
        static void CallMethod(object state)
        {
            Console.WriteLine("RunWorkerThread开始工作");
            Order order = state as Order;
            Console.WriteLine($"orderName:{order.orderName},price:{order.price}");
            Console.WriteLine("工作者线程启动成功!");
        }

        public static void Test()
        {
            //工作者线程最大数目，I/O线程的最大数目
            ThreadPool.SetMaxThreads(1000, 1000);
            Order order = new Order()
            {
                orderName = "冰箱",
                price = 1888
            };
            //启动工作者线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(CallMethod!), order);
            Console.ReadKey();
        }

    }
    public class Order
    {
        public string orderName { get; set; }
        public decimal price { get; set; }
    }
}
