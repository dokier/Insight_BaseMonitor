using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using System.Threading;

namespace Insight_BaseMonitor.Jobs
{
    [DisallowConcurrentExecution]
    class getCpuData : IMyJob
    {
        public string jobName
        {
            get { return "getCpuData"; }
            set { }
        }

        public string jobGroup
        {
            get { return "Misc"; }
            set { }
        }

        public int timeFrequency
        {
            get { return 1; }
            set { }
        }

        public void Execute(IJobExecutionContext context)
        {
             Thread.Sleep(2000);
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            var srvName = dataMap["srvName"];
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(srvName + "--> Executing Job to get CPU Information");
            Console.ResetColor();
        }
    }
}