using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole.Log
{ 
    public class FileLogger : LogBase
    {
        public  string LogType { get; set; }
        public FileLogger(string TypeName) 
        {
            LogType = TypeName;
        }
 
        public string filePath = $"c:\\temp\\log-{DateTime.Now.ToString("yyyy-MM-dd-HH")}.txt";
        public void Log(string ActionName, string message)
        {
            Log(LogType + "\r\n     " + ActionName + "\r\n     " + message);
        }
        public override void Log(string message)
        { 
            lock (lockObj)
            { 
                StreamWriter tw = null;
                if (File.Exists(filePath))
                {
                    using (tw = File.AppendText(filePath))
                    {
                        tw.WriteLine(DateTime.Now.ToString() + "> " + message);
                        tw.Flush();
                        tw.Close();
                    }
                }
                else
                {
                    tw = new StreamWriter(filePath);
                    tw.WriteLine(DateTime.Now.ToString() + "> " + message);
                    tw.Close();
                }
            }
        }
    }
}
