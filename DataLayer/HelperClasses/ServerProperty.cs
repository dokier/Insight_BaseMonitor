using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.HelperClasses
{
    public class ServerProperty
    {
        public string ComputerName { get; set; }
        public string InstanceName { get; set; }
        public string Edition { get; set; }
        public string ProductVersion { get; set; }
        public string ProductLevel { get; set; }
        public bool AuthMode { get; set; }
        public System.DateTime SQLInstallDate { get; set; }
        public string NetTransport { get; set; }
        public string AuthScheme { get; set; }
        public Int32 LocalTcpPort { get; set; }
        public bool Success { get; set; }
    }
}
