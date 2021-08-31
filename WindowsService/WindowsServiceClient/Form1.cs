using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsServiceClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string serviceFilePath = $"{Application.StartupPath}\\MyTestWindowsService.exe";
        string serviceName = "MyTestWindowsService";
        //事件：安装服务
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var ServiceNames = textBox1.Text;

                var serviceList = ServiceNames.Split(',');
                foreach (var item in serviceList)
                {

                }
                if (ServiceHelper.IsServiceExisted(serviceName)) ServiceHelper.UninstallService(serviceName);
                ServiceHelper.InstallService(serviceFilePath);
                MessageBox.Show($"{serviceName}服务安装成功。" ); ;
            }
            catch (Exception ex)
            { 
                MessageBox.Show("服务安装失败：" + ex.Message);
            }
      
        }
        //事件：启动服务
        private void button2_Click(object sender, EventArgs e)
        {           
            try
            { 
                if (ServiceHelper.IsServiceExisted(serviceName)) ServiceHelper.ServiceStart(serviceName);
                MessageBox.Show($"{serviceName}服务启动成功。"); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务启动失败：" + ex.Message);
            }
        }
        //事件：停止服务
        private void button3_Click(object sender, EventArgs e)
        { 
            try
            {
                if (ServiceHelper.IsServiceExisted(serviceName)) ServiceHelper.ServiceStop(serviceName);
                MessageBox.Show($"{serviceName}服务停止成功。"); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务停止失败：" + ex.Message);
            }
        }
        //事件：卸载服务
        private void button4_Click(object sender, EventArgs e)
        { 
            try
            {
                if (ServiceHelper.IsServiceExisted(serviceName))
                {
                    ServiceHelper.ServiceStop(serviceName);
                    ServiceHelper.UninstallService(serviceFilePath);
                }
                MessageBox.Show($"{serviceName}服务卸载成功。"); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务卸载失败：" + ex.Message);
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        { 
            ServiceListForm listForm = new ServiceListForm();
            listForm.Show();
        }

        private void RestartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServiceHelper.IsServiceExisted(serviceName))
                {
                    ServiceHelper.ServiceRestart(serviceName); 
                }
                MessageBox.Show($"{serviceName}服务重启成功。"); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务重启失败：" + ex.Message);
            }
        }
    }
}
