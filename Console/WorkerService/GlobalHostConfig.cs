using Framework.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkerService
{
    public class GlobalHostConfig
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
         
        public static RabbitMQOptions RabbitMQOptions { get; set; } = new RabbitMQOptions();

        public static List<KeyValuePair<string, string>> ConfigurationKeyValueList { get; set; } = new List<KeyValuePair<string, string>>();

        public static string wwwwroot { get { return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); } }
        public static string RootDirectory { get { return Directory.GetCurrentDirectory(); } }
    }
}
