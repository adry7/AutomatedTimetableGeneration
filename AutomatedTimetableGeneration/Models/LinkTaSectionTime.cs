//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutomatedTimetableGeneration.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LinkTaSectionTime
    {
        public int ID { get; set; }
        public string Ta_id { get; set; }
        public bool isSupport { get; set; }
        public int SectionTime_id { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual SectionTime SectionTime { get; set; }
    }
}
