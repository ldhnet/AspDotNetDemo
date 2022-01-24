using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Utility.Config
{
    public class GlobalConfig
    {
        /// <summary>
        /// All registered service and class instance container. Which are used for dependency injection.
        /// </summary>
        public static IServiceCollection Services { get; set; }
        /// <summary>
        /// Configured service provider.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        public static IConfiguration Configuration { get; set; }

        //public static IWebHostEnvironment HostingEnvironment { get; set; }

        public static SystemConfig SystemConfig { get; set; } = new SystemConfig();
        public static MailSenderOptions MailSenderOptions { get; set; } = new MailSenderOptions();
      
        public static string wwwwroot { get { return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); } }
        public static string RootDirectory { get { return Directory.GetCurrentDirectory(); } }
    }
}
