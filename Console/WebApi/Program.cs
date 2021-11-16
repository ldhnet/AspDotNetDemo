using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ���ö�ȡָ��λ�õ�nlog.config�ļ�
            NLogBuilder.ConfigureNLog("XmlConfig/nlog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseNLog();
    }
}
