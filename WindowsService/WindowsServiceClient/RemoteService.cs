using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsServiceClient
{
    public partial class RemoteService : Form
    {
        private string strPath;
        private ManagementClass managementClass;
        public RemoteService()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            startService();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                string colunmName = listView1.Columns[0].Text;//获取第一列的标题名称
                //获取选择行第一列的值
                string colunmVal1 = listView1.SelectedItems[0].SubItems[0].Text; 

            }
        }
   
        public void startService()
        {
            if (remoteConnectValidate("10.216.147.175", "lil", "Marykay22334455"))
            {
                win32ServiceManager("10.216.147.175", "lil", "Marykay22334455");

                this.listView1.Clear();
                this.listView1.View = View.Details;
                this.listView1.FullRowSelect = true;
                this.listView1.GridLines = true;
                this.listView1.Columns.Add("服务名称", 200, HorizontalAlignment.Left);
                this.listView1.Columns.Add("服务描述", 300, HorizontalAlignment.Left);
                this.listView1.Columns.Add("状态", 120, HorizontalAlignment.Left);
                this.listView1.Columns.Add("操作", 120, HorizontalAlignment.Left);


                this.listView1.Visible = true;
                string[,] services = getServiceList();
                for (int i = 0; i < services.GetLength(0); i++)
                { 
                    ListViewItem li = new ListViewItem();
                    li.SubItems[0].Text = services[i, 0];
                    li.SubItems.Add(services[i, 1]);
                    li.SubItems.Add(services[i, 2]);
                    li.SubItems.Add("修改");
                    this.listView1.Items.Add(li);
                }
                string serviceName = "IISADMIN";
                ManagementObject mo = this.managementClass.CreateInstance();
                mo.Path = new ManagementPath(this.strPath + ".Name=\"" + serviceName + "\"");
                //StartService 启动服务,PauseService 暂停服务,ResumeService 恢复服务,StopService 停止服务  
                mo.InvokeMethod("StartService", null);
            }
        }
        /// <summary>  
        /// 验证是否能连接到远程计算机    
        /// </summary>  
        /// <param name="host">主机名或ip</param>  
        /// <param name="userName">用户名</param>  
        /// <param name="password">密码</param>  
        /// <returns></returns>  
        public static bool remoteConnectValidate(string host, string userName, string password)
        {
            ConnectionOptions connectionOptions = new ConnectionOptions();
            connectionOptions.Username = userName;
            connectionOptions.Password = password;
            ManagementScope managementScope = new ManagementScope("\\\\" + host + "\\root\\cimv2", connectionOptions);
            try
            {
                managementScope.Connect();
            }
            catch
            {
            }
            return managementScope.IsConnected;
        }
        /// <summary>  
        /// 获取所连接的计算机的所有服务数据  
        /// </summary>  
        /// <returns></returns>  
        public string[,] getServiceList()
        {
            string[,] services = new string[this.managementClass.GetInstances().Count, 4];
            int i = 0;
            foreach (ManagementObject mo in this.managementClass.GetInstances())
            {
                services[i, 0] = (string)mo["Name"];
                services[i, 1] = (string)mo["DisplayName"];
                services[i, 2] = (string)mo["State"];
                services[i, 3] = (string)mo["StartMode"]; 
                i++;
            }
            return services;
        }


        private void win32ServiceManager(string host, string userName, string password)
        {
            this.strPath = "\\\\" + host + "\\root\\cimv2:Win32_Service";
            this.managementClass = new ManagementClass(strPath);
            if (userName != null && userName.Length > 0)
            {
                ConnectionOptions connectionOptions = new ConnectionOptions();
                connectionOptions.Username = userName;
                connectionOptions.Password = password;
                ManagementScope managementScope = new ManagementScope("\\\\" + host + "\\root\\cimv2", connectionOptions);
                this.managementClass.Scope = managementScope;
            }
        }

      
    }
}
