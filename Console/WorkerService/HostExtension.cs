using Framework.RabbitMQ;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService
{
    public static class HostExtension
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //判断系统是Windows还是linux
            bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (IsWindows)
                return Host.CreateDefaultBuilder(args)
                    .UseWindowsService() //使用windows服务
                    .ConfigureServices((hostContext, services) =>
                    {
                        GlobalHostConfig.Services = services;
                        GlobalHostConfig.Configuration = hostContext.Configuration;

                        hostContext.Configuration.Bind("RabbitMQOptions", GlobalHostConfig.RabbitMQOptions); 
                        services.AddRabbitMQ(option => option = GlobalHostConfig.RabbitMQOptions);//RabbitMQ

                        services.AddHostedService<Worker>(); //添加Worker
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Information);
                        logging.AddConsole();
                    }).UseNLog();//使用Nlog

            return Host.CreateDefaultBuilder(args)
                .UseSystemd()//使用Linux服务
                .ConfigureServices((hostContext, services) =>
                { 
                    services.AddHostedService<Worker>();//添加Worker
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                }).UseNLog();//使用Nlog
        }
    }
}
