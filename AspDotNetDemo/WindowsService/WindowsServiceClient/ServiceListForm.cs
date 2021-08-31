using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
namespace WindowsServiceClient
{
    public partial class ServiceListForm : Form
    {
        public ServiceListForm()
        {
            InitializeComponent();
            var serviceList = ServiceController.GetServices().ToList();
            GetServiceCount(serviceList.Count); 
        } 
        private void GetServiceCount(int _count)
        {
            this.labelCount.Text = $"共：{_count.ToString()} 个服务";
        }
        private void RefreshBtn_Click_1(object sender, EventArgs e)
        {
            RefreshAction();
        }
        private void RefreshAction()
        {
            var serviceList = ServiceController.GetServices().ToList(); 

            ServiceLoadList(serviceList);
        }
        private void ServiceList_Load(object sender, EventArgs e)
        {   
            var serviceList = ServiceController.GetServices().ToList();
            GetServiceCount(serviceList?.Count ?? 0);
            ServiceLoadList(serviceList);
        }

        private void ServiceLoadList(List<ServiceController> list)
        {
            this.listView1.Clear();
            this.listView1.View = View.Details;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Columns.Add("服务名称", 200, HorizontalAlignment.Left);
            this.listView1.Columns.Add("服务描述", 300, HorizontalAlignment.Left);
            this.listView1.Columns.Add("状态", 120, HorizontalAlignment.Left);
            this.listView1.Columns.Add("操作", 120, HorizontalAlignment.Left);
            this.listView1.Visible = true; 
            foreach (var tt in list)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = tt.ServiceName;
                li.SubItems.Add(tt.DisplayName);
                li.SubItems.Add(tt.Status.ToString());
                li.SubItems.Add("修改");
                this.listView1.Items.Add(li);
            }
        }
         
        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                //获取选择行第一列的值
                string serviceName = listView1.SelectedItems[0].SubItems[0].Text;

                try
                {
                    if (ServiceHelper.IsServiceExisted(serviceName)) ServiceHelper.ServiceStart(serviceName);
                    this.RefreshAction(); 
                    MessageBox.Show($"{serviceName}服务启动成功。");
              
                }
                catch (Exception ex)
                {
                    MessageBox.Show("服务启动失败：" + ex.Message);
                }
            }
        }
    
        private void RestartBtn_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                string serviceName = listView1.SelectedItems[0].SubItems[0].Text;
                try
                {
                    if (ServiceHelper.IsServiceExisted(serviceName))
                    {
                        ServiceHelper.ServiceRestart(serviceName);
                        this.RefreshAction();
                    }
                    MessageBox.Show($"{serviceName}服务重启成功。"); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("服务重启失败：" + ex.Message);
                }
            }        
        }

        private void StopBtn_Click_1(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                string serviceName = listView1.SelectedItems[0].SubItems[0].Text;
                try
                {
                    if (ServiceHelper.IsServiceExisted(serviceName)) ServiceHelper.ServiceStop(serviceName);
                    this.RefreshAction();
                    MessageBox.Show($"{serviceName}服务停止成功。"); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("服务停止失败：" + ex.Message);
                }
            }
        }

        private void UninstallBtn_Click_1(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                string serviceName = listView1.SelectedItems[0].SubItems[0].Text;
                try
                {
                    if (ServiceHelper.IsServiceExisted(serviceName))
                    {
                        ServiceHelper.ServiceStop(serviceName);
                        ServiceHelper.UninstallService(serviceName);
                        this.RefreshAction();
                    }
                    MessageBox.Show($"{serviceName}服务卸载成功。"); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("服务卸载失败：" + ex.Message);
                }
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            var searchTest = this.SearchText.Text;
            var serviceList = ServiceController.GetServices().Where(c=>c.ServiceName.Contains(searchTest) || c.DisplayName.Contains(searchTest)).ToList();
            GetServiceCount(serviceList?.Count ?? 0);
            ServiceLoadList(serviceList);
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

        private static bool flag = false;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var serviceList =new List<ServiceController>(); 
            if (!flag)
            {
                flag = true;
                switch (e.Column)
                {
                    case 0:
                        serviceList = ServiceController.GetServices().OrderBy(c => c.ServiceName).ToList();
                        break;
                    case 1:
                        serviceList = ServiceController.GetServices().OrderBy(c => c.DisplayName).ToList();
                        break;
                    case 2:
                        serviceList = ServiceController.GetServices().OrderBy(c => c.Status).ToList();
                        break;
                    default:
                        serviceList = ServiceController.GetServices().OrderBy(c => c.ServiceName).ToList();
                        break;
                }
            }
            else
            {
                flag = false;
                switch (e.Column)
                {
                    case 0:
                        serviceList = ServiceController.GetServices().OrderByDescending(c => c.ServiceName).ToList();
                        break;
                    case 1:
                        serviceList = ServiceController.GetServices().OrderByDescending(c => c.DisplayName).ToList();
                        break;
                    case 2:
                        serviceList = ServiceController.GetServices().OrderByDescending(c => c.Status).ToList();
                        break;
                    default:
                        serviceList = ServiceController.GetServices().OrderByDescending(c => c.ServiceName).ToList();
                        break;
                }
            } 
            ServiceLoadList(serviceList);
        }
    }
}
