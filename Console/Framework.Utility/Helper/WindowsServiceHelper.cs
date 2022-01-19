using Microsoft.Win32;
using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceClient
{
    //public class WindowsServiceHelper
    //{

    //    //判断服务是否存在
    //    public static bool IsServiceExisted(string serviceName)
    //    {
    //        ServiceController[] services = ServiceController.GetServices();
    //        foreach (ServiceController sc in services)
    //        {
    //            if (sc.ServiceName.ToLower() == serviceName.ToLower())
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    //安装服务
    //    public static void InstallService(string serviceFilePath)
    //    {
    //        using (AssemblyInstaller installer = new AssemblyInstaller())
    //        {
    //            installer.UseNewContext = true;
    //            installer.Path = serviceFilePath;
    //            IDictionary savedState = new Hashtable();
    //            installer.Install(savedState);
    //            installer.Commit(savedState);
    //        }
    //    }

    //    //卸载服务
    //    public static void UninstallService(string serviceName)
    //    {
    //        string key = @"SYSTEM\CurrentControlSet\Services\" + serviceName;
    //        string path = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();
    //        //替换掉双引号   
    //        path = path.Replace("\"", string.Empty);

    //        FileInfo fi = new FileInfo(path); 
            
    //        var serviceFilePath= fi.Directory.ToString();

    //        using (AssemblyInstaller installer = new AssemblyInstaller())
    //        { 
    //            installer.UseNewContext = true;
    //            installer.Path = "";
    //            installer.Uninstall(null);
    //        }
    //    }
    //    //启动服务
    //    public static void ServiceStart(string serviceName)
    //    {
    //        using (ServiceController control = new ServiceController(serviceName))
    //        {
    //            if (control.Status == ServiceControllerStatus.Stopped)
    //            {
    //                control.Start();
    //                control.WaitForStatus(ServiceControllerStatus.Running);
    //            }
    //        }
    //    }

    //    //停止服务
    //    public static void ServiceStop(string serviceName)
    //    {
    //        using (ServiceController control = new ServiceController(serviceName))
    //        {
    //            if (control.Status == ServiceControllerStatus.Running)
    //            {
    //                control.Stop();
    //                control.WaitForStatus(ServiceControllerStatus.Stopped);
    //            }
    //        }
    //    }

    //    //重启服务
    //    public static void ServiceRestart(string serviceName)
    //    {
    //        using (ServiceController control = new ServiceController(serviceName))
    //        {
    //            if (control.Status == ServiceControllerStatus.Running)
    //            {
    //                control.Stop();
    //                control.WaitForStatus(ServiceControllerStatus.Stopped);
    //            }
    //            control.Start();
    //            control.WaitForStatus(ServiceControllerStatus.Running);

    //        }
    //    }
    //}
}
