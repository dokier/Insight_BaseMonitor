using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insight_BaseMonitor.Jobs
{
    internal interface IMyJob : IJob
    {
        string jobName { get; set; }

        string jobGroup { get; set; }

        int timeFrequency { get; set; }

        new void Execute(IJobExecutionContext context);
    }
}
