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
    
    public partial class Backup
    {
        public int Id { get; set; }
        public string DBName { get; set; }
        public Nullable<System.DateTime> TapeFull { get; set; }
        public Nullable<System.DateTime> TapeDiff { get; set; }
        public Nullable<System.DateTime> DiskFull { get; set; }
        public Nullable<System.DateTime> DiskDiff { get; set; }
        public Nullable<System.DateTime> DiskTlog { get; set; }
        public string RecoveryModel { get; set; }
        public string StateDesc { get; set; }
        public string Updateability { get; set; }
        public System.DateTime RunDate { get; set; }
        public int RunCount { get; set; }
        public int InstanceId { get; set; }
    
        public virtual Instance Instance { get; set; }
    }
}
