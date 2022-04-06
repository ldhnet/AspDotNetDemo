using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class ProcessAndThread
    {
        public static void TestMain()
        {
            GetProcess();
            GetThread();
            GetIPList();
        }

        public static void GetProcess()
        { 
            //当前机器所有进程
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                Console.WriteLine(process.Id);
                Console.WriteLine(process.ProcessName);
            }

            Console.WriteLine("***********ProcessId***********");

            Process processes2 = Process.GetCurrentProcess();

            Console.WriteLine(processes2.ProcessName);
            Console.WriteLine(processes2.Id); 

        }
        public static void GetThread()
        {
            Console.WriteLine("***********ThreadId***********");
            var ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();

            Console.WriteLine(ThreadId);

            var threads = Process.GetCurrentProcess().Threads;
            var count = threads.Count;
            var activedList = threads.Cast<ProcessThread>().ToList();
            //.Where(t => t.ThreadState == System.Diagnostics.ThreadState.Running)

            Console.WriteLine("***********Thread Count***********");
            Console.WriteLine(count);


            Console.WriteLine("***********Thread List***********");
            foreach (var item in activedList)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.ThreadState);
            }
        }
        public static void GetIPList()
        {
            string name = Dns.GetHostName();

            Console.WriteLine("设备名称=" + name);

            IPAddress[] ips = Dns.GetHostAddresses(name);
            List<string> list = new List<string>();
            foreach (IPAddress ip in ips)
            {
                if (Regex.IsMatch(ip.ToString(), @"^\d+.\d+.\d+.\d+$"))
                {
                    list.Add(ip.ToString());
                }
            }

            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
        }


        public static void GetSystemId()
        {
            string systemId = null;
            //using (ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_ComputerSystemProduct"))
            //{
            //    foreach (var item in mos.Get())
            //    {
            //        systemId = item["UUID"].ToString();
            //    }
            //}
           Console.WriteLine(systemId);
        }
    }


}
