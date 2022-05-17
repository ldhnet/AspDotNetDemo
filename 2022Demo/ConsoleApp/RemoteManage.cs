using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class RemoteManage
    {
        #region Windows Service 操作
       
        /// <summary>
        /// 修改windows 服务的显示名称和描述
        /// </summary>
        /// <param name="webIp"></param>
        /// <param name="serviceName"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        public void UpdateDisplayNameAndDescription(string webIp, string serviceName, string displayName, string description)
        {
            //检查服务是否存在
            string imagePath = GetImagePath(webIp, serviceName);
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new Exception($"服务{serviceName}在{webIp}不存在");
            }
            //生成修改指令
            string updateDispalyNameCommand = $"sc \\\\{webIp} config {serviceName} displayname= \"{displayName}\"";
            string updateDescriptionCommand = $"sc \\\\{webIp} description {serviceName} \"{description}\" ";
            string[] cmd = new string[] { updateDispalyNameCommand, updateDescriptionCommand };
            string ss = Cmd(cmd);
        }

        /// <summary>
        /// sc 停止和启动windows服务
        /// </summary>
        /// <param name="webIp"></param>
        /// <param name="serviceName"></param>
        /// <param name="stop"></param>
        public void StopAndStartService(string webIp, string serviceName, bool stop)
        {
            string serviceCommand = $"sc \\\\{webIp} {(stop ? "stop" : "start")} {serviceName}";//停止或启动服务  
            string[] cmd = new string[] { serviceCommand };
            string ss = Cmd(cmd);
        }

        /// <summary>  
        /// 运行CMD命令  
        /// </summary>  
        /// <param name="cmd">命令</param>  
        /// <returns></returns>  
        public string Cmd(string[] cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.AutoFlush = true;
            for (int i = 0; i < cmd.Length; i++)
            {
                p.StandardInput.WriteLine(cmd[i].ToString());
            }
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            return strRst;
        }

        /// <summary>
        /// 关闭远程计算机进程
        /// </summary>
        /// <param name="webIp"></param>
        /// <param name="processName"></param>
        public void CloseProcess(string webIp, string processName, string executablePath)
        {
            //ConnectionOptions oOptions = new ConnectionOptions();
            //oOptions.Authentication = AuthenticationLevel.Default;
            //ManagementPath oPath = new ManagementPath($"\\\\{webIp}\\root\\cimv2");
            //ManagementScope oScope = new ManagementScope(oPath, oOptions);
            //ObjectQuery oQuery = new ObjectQuery($"Select * From Win32_Process Where Name = \"{processName}\"");
            //using (ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oScope, oQuery))
            //{
            //    foreach (ManagementObject oManagementObject in oSearcher.Get())
            //    {
            //        if (oManagementObject["Name"].ToString().ToLower() == processName.ToLower())
            //        {
            //            /*
            //                foreach (PropertyData prop in oManagementObject.Properties)
            //                {
            //                    Console.WriteLine($"      {prop.Name} -- -  {prop.Value } ");
            //                }
            //             */
            //            string path = oManagementObject["ExecutablePath"].ToString();
            //            if (string.IsNullOrEmpty(executablePath) || path == executablePath)
            //            {
            //                oManagementObject.InvokeMethod("Terminate", new object[] { 0 });
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 获取远程计算机 服务的执行文件
        /// </summary>
        /// <param name="serverIP">远程计算机IP</param>
        /// <param name="serviceName">远程服务名称</param>
        /// <returns></returns>
        public string GetImagePath(string serverIP, string serviceName)
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + serviceName;
            using (RegistryKey key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, serverIP).OpenSubKey(registryPath))
            {
                if (key == null)
                {
                    return string.Empty;
                }
                string value = key.GetValue("ImagePath").ToString();
                key.Close();
                value = value.Trim('"');
                if (value.Contains("SystemRoot"))
                {
                    return ExpandEnvironmentVariables(serverIP, value);
                }
                return value;
            }
        }

        /// <summary>
        /// 替换路径中的SystemRoot
        /// </summary>
        /// <param name="serverIP">远程计算机IP</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private string ExpandEnvironmentVariables(string serverIP, string path)
        {
            string systemRootKey = @"Software\Microsoft\Windows NT\CurrentVersion\";
            using (RegistryKey key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, serverIP).OpenSubKey(systemRootKey))
            {
                string expandedSystemRoot = key.GetValue("SystemRoot").ToString();
                key.Close();
                path = path.Replace("%SystemRoot%", expandedSystemRoot);
                return path;
            }
        }

        /// <summary>
        /// 停止或启动Windows 服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serverIP">远程IP</param>
        /// <param name="stop">是否是停止</param>
        public void StopAndStartWindowsService(string serviceName, string serverIP, bool stop)
        {
            using (ServiceController sc = ServiceController.GetServices(serverIP)
                            .FirstOrDefault(x => x.ServiceName == serviceName))
            {
                if (sc == null)
                {
                    throw new Exception($"{serviceName}不存在于{serverIP}");
                }
                StopAndStartWindowsService(sc, stop);

                if (stop)
                {
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 1, 0));
                }
                sc.Close();
            }


        }

        /// <summary>
        /// 停止或启动Windows 服务
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="stop"></param>
        private void StopAndStartWindowsService(ServiceController sc, bool stop = true)
        {
            Action act = () =>
            {
                ServiceControllerStatus serviceSate = sc.Status;
                if (stop && (serviceSate == ServiceControllerStatus.StartPending || serviceSate == ServiceControllerStatus.Running))
                {
                    //如果当前应用程序池是启动或者正在启动状态，调用停止方法
                    sc.Stop();
                }
                if (!stop && (serviceSate == ServiceControllerStatus.Stopped || serviceSate == ServiceControllerStatus.StopPending))
                {
                    sc.Start();
                }
            };
            int retryCount = 0;
            int maxCount = 4;
            while (sc != null && retryCount <= maxCount)
            {
                try
                {
                    act();
                    break;
                }
                catch (Exception ex)
                {
                    if (stop)
                    {
                        string serverIP = sc.MachineName;
                        string serviceName = sc.ServiceName;
                        var imeagePath = GetImagePath(serverIP, serviceName);
                        FileInfo fileInfo = new FileInfo(imeagePath);
                        CloseProcess(serverIP, fileInfo.Name, fileInfo.FullName);
                    }
                    retryCount++;
                    if (retryCount == maxCount)
                    {
                        throw new Exception($"{(stop ? "停止" : "启动")}Windows服务{sc.ServiceName}出错{ex.Message}");
                    }
                    Thread.Sleep(1000 * 30);
                }
            }//end while
        }

        /// <summary>
        /// 获取windows 服务状态
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serverIP">服务器IP</param>
        /// <returns></returns>
        public ServiceControllerStatus GetServiceState(string serviceName, string serverIP)
        {
            ServiceControllerStatus serviceSate;
            using (ServiceController sc = ServiceController.GetServices(serverIP)
                            .FirstOrDefault(x => x.ServiceName == serviceName))
            {
                if (sc == null)
                {
                    throw new Exception($"{serviceName}不存在于{serverIP}");
                }
                serviceSate = sc.Status;
                sc.Close();
            }
            return serviceSate;
        }

        #endregion
    }
}
