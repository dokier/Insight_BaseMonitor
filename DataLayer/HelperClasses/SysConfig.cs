using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.HelperClasses
{
    public class SysConfig
    {
        public Int32 MaxServerMemory { get; set; }
        public Int32 MinServerMemory { get; set; }
        public Boolean BackupCompression { get; set; }
        public Int16 MaxDop { get; set; }
        public Boolean Xp_Cmdshell { get; set; }
        public bool Success { get; set; }
    }
}

