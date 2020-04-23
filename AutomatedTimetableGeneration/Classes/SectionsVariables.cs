using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class SectionsVariables
    {
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        //public string LaborSection { get; set; }
        public bool HaveLab { get; set; }
        public bool HaveSections { get; set; }
        public int LabHours { get; set; }
        public int SectionHours { get; set; }
        public SectionsVariables(int sectionId, int courseId, int groupId, bool haveLab, bool haveSections, int labHours,int sectionHours)
        {
            SectionId = sectionId;
            CourseId = courseId;
            GroupId = groupId;
            HaveLab = haveLab;
            HaveSections = haveSections;
            LabHours = labHours;
            SectionHours = sectionHours;
        }
    }
}