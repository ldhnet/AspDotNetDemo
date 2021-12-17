using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkerService
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
                        //GlobalVariable.TcPServerIP = hostContext.Configuration.GetSection("TcpServer")["ip"];
                        //GlobalVariable.TcpServerPort = hostContext.Configuration.GetSection("TcpServer")["port"];
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
                    //GlobalVariable.TcPServerIP = hostContext.Configuration.GetSection("TcpServer")["ip"];
                    //GlobalVariable.TcpServerPort = hostContext.Configuration.GetSection("TcpServer")["port"];
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
