using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ScheduleTask
    {
        public void ScheduleTaskMain()
        {
            string ip = "110.42.176.117";
            string username = "root";
            string password = "***";
            string installSelectorPath = "cd /root/ldh";

            string creatTaskArg = string.Format(@" /create /s {0} -u domainname\{1} -p {2} /sc ONCE /st 10:00 /tn installSelector /tr {3} /rl HIGHEST /ru Local /IT", ip, username, password, installSelectorPath);
            string runTaskArg = string.Format(@" /run /s {0} -u domainname\{1} -p {2} /tn installSelector", ip, username, password); ;
            string deleteTaskArg = string.Format(@" /delete /s {0} -u domainname\{1} -p {2} /tn installSelector /F", ip, username, password);

            System.Diagnostics.Process p1 = new System.Diagnostics.Process();
            p1.StartInfo.FileName = @"schtasks.exe";
            p1.StartInfo.Arguments = string.Format(@" /query /s {0} -u domainname\{1} -p {2} /tn installSelector", ip, username, password);
            p1.StartInfo.UseShellExecute = false;
            p1.StartInfo.RedirectStandardError = true;
            p1.StartInfo.RedirectStandardOutput = true;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.WaitForExit();

            //当前机器所有进程
  

            string err = p1.StandardError.ReadToEnd();
            string sop = p1.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(err) && string.IsNullOrEmpty(sop))
            {

                p1.StartInfo.Arguments = creatTaskArg;
                p1.Start();
                p1.WaitForExit();
                err = p1.StandardError.ReadToEnd();
                sop = p1.StandardOutput.ReadToEnd();
                if (!sop.ToLower().Contains("success"))
                {
                    throw new Exception(string.Format("Create schedule task failed on {0}", ip));
                }

            }
            else
            {
                Console.WriteLine(err);
            }

            p1.StartInfo.Arguments = runTaskArg;
            p1.Start();
            p1.WaitForExit();
            err = p1.StandardError.ReadToEnd();
            sop = p1.StandardOutput.ReadToEnd();

            if (!string.IsNullOrEmpty(err) || !sop.ToLower().Contains("success"))
            {
                throw new Exception(string.Format("Run schedule task failed on {0}", ip));
            }

            p1.StartInfo.Arguments = deleteTaskArg;
            p1.Start();
            p1.WaitForExit();
            err = p1.StandardError.ReadToEnd();
            sop = p1.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(err) || !sop.ToLower().Contains("success"))
            {
                throw new Exception(string.Format("Delete schedule task failed on {0}", ip));
            }
            p1.Close();
        }
    }
}
