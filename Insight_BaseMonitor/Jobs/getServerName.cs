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
    class getServerName : IMyJob
    {
        public string jobName
        {
            get { return "getServerName"; }
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
            Thread.Sleep(1000);
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            var srvName = dataMap["srvName"];
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(srvName + "--> Executing Job to get Server Name");
            Console.ResetColor();
        }
    }
}
