using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Config
{
    public class GlobalConfig
    {
        public static SystemConfig SystemConfig { get; set; } = new SystemConfig();
         
        /// <summary>
        /// Configured service provider.
        /// </summary>
        public static IServiceProvider? ServiceProvider { get; set; } 
    }
}
