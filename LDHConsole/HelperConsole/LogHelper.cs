using HelperConsole.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole
{

    public class LogHelper
    {
        public void WriteLog()
        {
            LogManager log = new LogManager(typeof(Program));

            for (int i = 0; i < 5; i++)
            {
                log.DeBugLog("Hello Task2早上好 Task2早上好 Task2早上好");
                log.WarningLog("Hello Task2早上好 Task2早上好 Task2早上好");
                log.ErrorLog("Hello Task2早上好 Task2早上好 Task2早上好");
            }
        }
    }
}
