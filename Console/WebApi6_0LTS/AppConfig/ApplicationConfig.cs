using Framework.Utility.Config;
using Framework.Utility.Helper;

namespace WebApi6_0.AppConfig
{
    public class ApplicationConfig
    {
        public static void OnAppStarted()
        {
            NLogHelper.Error($"应用程序启动{DateTime.Now}");
            var _configuration = GlobalConfig.ServiceProvider!.GetService<IConfiguration>()!;

            var url = _configuration[WebHostDefaults.ServerUrlsKey];

            var warm = url.Split(';')[0] + "/warm";

            new HttpClient().GetAsync(warm).Wait();
          
        }
        public static void OnAppStopped()
        {
            NLogHelper.Error($"应用程序关闭{DateTime.Now}");
        }
    }
}
