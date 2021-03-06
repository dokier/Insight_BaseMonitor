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
    
    public partial class DatabaseOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Compatibility { get; set; }
        public string StateDesc { get; set; }
        public string UserAccessDesc { get; set; }
        public string RecoveryModel { get; set; }
        public string Collation { get; set; }
        public string PageVerify { get; set; }
        public bool ReadOnly { get; set; }
        public bool AutoClose { get; set; }
        public bool AutoShrink { get; set; }
        public bool AutoCreateStats { get; set; }
        public bool AutoUpdateStats { get; set; }
        public bool FullText { get; set; }
        public bool DbChaining { get; set; }
        public bool Trustworthy { get; set; }
        public string Owner { get; set; }
        public System.DateTime RunDate { get; set; }
        public int RunCount { get; set; }
        public int InstanceId { get; set; }
    
        public virtual Instance Instance { get; set; }
    }
}
