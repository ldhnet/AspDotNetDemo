using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceTest
{
    internal static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            BackgroundWorker();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
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
