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
    
    public partial class DatabaseFile
    {
        public int Id { get; set; }
        public string DBName { get; set; }
        public string LogicalName { get; set; }
        public string Type { get; set; }
        public int Total_Space_MB { get; set; }
        public decimal Used_Space_MB { get; set; }
        public decimal Free_Space_MB { get; set; }
        public decimal PercentUsed { get; set; }
        public string PhysicalName { get; set; }
        public string FileGroup { get; set; }
        public string FileGrowth { get; set; }
        public string AutoGrowth { get; set; }
        public System.DateTime RunDate { get; set; }
        public int RunCount { get; set; }
        public int InstanceId { get; set; }
    
        public virtual Instance Instance { get; set; }
    }
}
