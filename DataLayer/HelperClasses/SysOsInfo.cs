using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.HelperClasses
{
    public class SysOsInfo
    {
        public Int32 CpuCount { get; set; }
        public Int64 ServerMemory { get; set; }
        public DateTime? SQLStartTime { get; set; }
        public String MachineType { get; set; }
        public bool Success { get; set; }

    }
}

