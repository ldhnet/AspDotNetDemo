using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Hangfire
{
    public class HangfirePrefix
    {
        private readonly string _applicationName;
         
        //private HangfirePrefix() { } 

        public HangfirePrefix(string applicationName)
        {
            _applicationName = applicationName;
        }

        public string GetHangfirePrefix()
        {
            return $"hangfire:{_applicationName.ToLower()}";
        }

    }
}
