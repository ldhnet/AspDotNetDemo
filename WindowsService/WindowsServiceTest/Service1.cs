using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceTest
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimeEvent);
            timer.AutoReset = true;
            timer.Enabled = true;
            int minute = 1;
            timer.Interval = (1000 * 60) * minute;        //分钟
            timer.Start();

        }

        protected override void OnStop()
        {
            //: 在此处添加代码以执行停止服务所需的关闭操作。
            this.timer.Enabled = false;
        }
        private void TimeEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            WriteTest();
        }

        public void WriteTest()
        {
           Debug.Write("*******************测试**************************");
        }
    }
}
