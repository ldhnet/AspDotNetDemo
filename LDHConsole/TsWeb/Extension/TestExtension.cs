using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TsWeb.Extension
{
    public static class TestExtension
    { 
        public static async void AddTestServer(this IServiceCollection services)
        {
            //[this IServiceCollection services]实现在Startup中的调用方法：services.AddTestServer
            //_param = param;//将参数赋给全局变量，用于其他方法使用
            //以下为方法的实现

            BackgroundWorker();
        }
        private static void BackgroundWorker()
        {
            var worker = new System.ComponentModel.BackgroundWorker();

            worker.DoWork += (sender, e) =>
            {
                int sum = 0;
                for (int i = 0; i <= 100; i++)
                {
                    sum += i;

                    System.Diagnostics.Debug.WriteLine($"-----单元测试 {i} -----");
                }
                System.Threading.Thread.Sleep(3000);
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                worker.RunWorkerAsync();
            };

            worker.RunWorkerAsync();

        }
    }
}
