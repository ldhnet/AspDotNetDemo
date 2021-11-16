using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace WebMVC.Extension
{
    public class CoreHttpContext
    {
        private static IWebHostEnvironment _hostEnviroment;
        public static string WebPath => _hostEnviroment.WebRootPath;

        public static string MapPath(string path)
        {
            return Path.Combine(_hostEnviroment.WebRootPath, path);
        }

        internal static void Configure(IWebHostEnvironment hostEnviroment)
        {
            _hostEnviroment = hostEnviroment;
        }
    }
    public static class StaticHostEnviromentExtensions
    {
        public static IApplicationBuilder UseStaticHostEnviroment(this IApplicationBuilder app)
        {
            var webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            CoreHttpContext.Configure(webHostEnvironment);
            return app;
        }
    }

}
