using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Config
{
    public class GlobalConfig
    {
        public static SystemConfig SystemConfig { get; set; } = new SystemConfig();
    }
}
