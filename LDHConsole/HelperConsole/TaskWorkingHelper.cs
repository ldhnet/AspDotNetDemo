using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using HelperConsole.Interface;

namespace HelperConsole
{
   
    public class TaskWorking : IDisposable
    {
 
        private IPlanningTaskInterface planningTaskContract;
        /// <summary>
        /// 系统计时器，默认间隔时间为1/4个小时
        /// </summary>
        Timer Retriever = new Timer(1000 * 60 * 1); //15

        public TaskWorking(IPlanningTaskInterface _planningTaskContract)
        {
            planningTaskContract = _planningTaskContract;
            Retriever.Elapsed += Retriever_Elapsed;
            Retriever.AutoReset = true;
            //planningTaskContract.Do();
        }

        void Retriever_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO
            planningTaskContract.Do();
        }


        public void Start()
        {
            Retriever.Enabled = true;
            Retriever.Start();
        }

        public void Stop()
        {
            Retriever.Stop();
            Retriever.Enabled = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                Retriever.Close();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
