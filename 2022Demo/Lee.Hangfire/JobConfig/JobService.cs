using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Hangfire
{
    public class JobService: IJobService
    {
        private readonly IServiceProvider _service;
        public  JobService(IServiceProvider service)
        {
            _service = service;
        }
        public void EnqueueTasks()
        {
            var jobs = _service.GetServices<IJob>();
            foreach (var job in jobs)
            {
                var attr = job.GetType().IsAssignableTo(typeof(JobConfigAbstract)); 
                if (!attr) continue;
                var task = job as JobConfigAbstract;
                if (!string.IsNullOrEmpty(task!.JobId))
                {
                    if (task.Enable)
                        RecurringJob.AddOrUpdate(task.JobId, () => job.Execute(), task.CronExpression, TimeZoneInfo.Local, task.Queue);
                    else
                        RecurringJob.RemoveIfExists(task.JobId);
                }
            }
        }
    }
}
