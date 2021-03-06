//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Insight_BL
{
    using System;
    using System.Collections.Generic;
    
    public partial class InstanceDetail
    {
        public int Id { get; set; }
        public string Edition { get; set; }
        public string Version { get; set; }
        public string ServicePack { get; set; }
        public string MachineType { get; set; }
        public bool AuthMode { get; set; }
        public int TcpPort { get; set; }
        public Nullable<bool> BackupCompression { get; set; }
        public string PowerPlan { get; set; }
        public short MaxDOP { get; set; }
        public bool Xp_Cmdshell { get; set; }
        public int MaxServerMemory_MB { get; set; }
        public int MinServerMemory_MB { get; set; }
        public long ServerMemory_MB { get; set; }
        public int CPU_Count { get; set; }
        public System.DateTime InstallDate { get; set; }
        public Nullable<System.DateTime> LastStartDate { get; set; }
        public System.DateTime RunDate { get; set; }
        public int RunCount { get; set; }
        public int InstanceId { get; set; }
    
        public virtual Instance Instance { get; set; }
    }
}
