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
    
    public partial class LinkDoctorCourse
    {
        public int ID { get; set; }
        public string Doctor_id { get; set; }
        public int Course_id { get; set; }
        public Nullable<int> hours { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Course Course { get; set; }
    }
}