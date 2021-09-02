using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole.Log
{
    public abstract class LogBase
    {
        protected readonly object lockObj = new object(); 
        public abstract void Log(string message);
    }
}
