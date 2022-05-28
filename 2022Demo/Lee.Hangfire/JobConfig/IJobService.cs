using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Hangfire
{
    public interface IJobService
    {
        void EnqueueTasks();
    }
}
