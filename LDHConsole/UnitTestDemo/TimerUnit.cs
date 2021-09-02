using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo
{
    [TestClass]
    public class TimerUnit
    {
        System.Timers.Timer _timer;
        [TestMethod]
        public void TestTimersTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000; //单位毫秒
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Trace.WriteLine("Test System.Timers.Timer");
                #region 此处展示的是把副线程的内容转到主线程

                Trace.WriteLine("Test System.Timers.Timer22222222");
                //System.Windows.Application.Current.Dispatcher.Invoke(
                //         new Action(() =>
                //         {
                //             Trace.WriteLine("同步切换");
                //         }));
                //System.Windows.Application.Current.Dispatcher.BeginInvoke(
                //         new Action(() =>
                //         {
                //             Trace.WriteLine("异步切换");
                //         }));
                #endregion
                _timer.Stop();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error");
                _timer.Stop();
                _timer.Start();
            }
        }
    }

   
}
