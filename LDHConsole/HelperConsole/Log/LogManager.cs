using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole.Log
{
    public enum LogType
    {
        DeBug, Warning, Error
    }
    public  class LogManager
    {
        private FileLogger logger = null;
        public  string ActionName { get; set; }
        public LogManager(Type type)
        {
            ActionName = type.FullName;
        }
        public  void DeBugLog(string message)
        {
            logger = new FileLogger("DeBug");
            logger.Log(ActionName,message);
        }
        public  void WarningLog(string message)
        {
            logger = new FileLogger("Warning");
            logger.Log(ActionName, message);
        }
        public  void ErrorLog(string message)
        {
            logger = new FileLogger("Error");
            logger.Log(ActionName, message);
        }
    }
}
