using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole.Interface
{
    public partial class PlanningTaskInterface
    {
        private void Task1(DateTime date, bool isfirsttime)
        {
            // FileMode.CreateNew: 如果文件不存在，创建文件；如果文件已经存在，抛出异常 
            //FileStream fS = new FileStream(@"C:\temp\tasklog.txt", FileMode.CreateNew, FileAccess.Write, FileShare.Read);
           
            StreamWriter sW = new StreamWriter(@"c:\temp\tasklog.txt", true, Encoding.UTF8);

            string[] strs = { $"Task1早上好{date.ToString()}", $"Task1下午好{date.ToString()}", $"Task1晚上好{date.ToString()}" };            
            foreach (var s in strs)
            {
                sW.WriteLine(s);
            }
            sW.Close();
        }
        private void Task2(DateTime date, bool isfirsttime)
        {
            StreamWriter sW = new StreamWriter(@"c:\temp\tasklog.txt", true, Encoding.UTF8);
            string[] strs = { $"Task2早上好{date.ToString()}", $"Task2下午好{date.ToString()}", $"Task2晚上好{date.ToString()}" };
            foreach (var s in strs)
            {
                sW.WriteLine(s);
            }
            sW.Close();
        }
        private void Task3(DateTime date, bool isfirsttime)
        {
            StreamWriter sW = new StreamWriter(@"c:\temp\tasklog.txt", true, Encoding.UTF8);
            string[] strs = { $"Task3早上好{date.ToString()}", $"Task3下午好{date.ToString()}", $"Task3晚上好{date.ToString()}" };
            foreach (var s in strs)
            {
                sW.WriteLine(s);
            }
            sW.Close();
        }


    }
}
