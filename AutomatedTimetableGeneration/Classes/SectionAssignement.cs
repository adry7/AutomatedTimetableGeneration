
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class SectionAssignement
    {
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }
      //  public string LaborSection { get; set; }
        public int Nofhours { get; set; }
        public int Start { get; set; }
        public int Day { get; set; }
        public int RoomId { get; set; }
        //public string Ta1id { get; set; }
        //public string Ta2id { get; set; }
        public List<string> Tasids { get; set; }
        public bool IsLab { get; set; }
        public bool IsSection { get; set; }

        //public SectionAssignement(int sectionId, int courseId, int groupId, string laborSection, int nofhours, int start, int day, int roomId, string ta1id, string ta2id)
        //{
        //    SectionId = sectionId;
        //    CourseId = courseId;
        //    GroupId = groupId;
        //    LaborSection = laborSection;
        //    Nofhours = nofhours;
        //    Start = start;
        //    Day = day;
        //    RoomId = roomId;
        //    Ta1id = ta1id;
        //    Ta2id = ta2id;
        //}

        public SectionAssignement(SectionsVariables UnSection  ,int start, int day, int roomId, List<string> ta2id,bool ISlab,bool ISsection)
        {
            SectionId = UnSection.SectionId;
            CourseId = UnSection.CourseId;
            GroupId = UnSection.GroupId;
            IsLab = ISlab;
            IsSection = ISsection;
            if (UnSection.HaveLab)
                Nofhours = UnSection.LabHours;


            else
                Nofhours = UnSection.SectionHours;
            Start = start;
            Day = day;
            RoomId = roomId;
            Tasids = ta2id;
            IsLab = UnSection.HaveLab;
            IsSection = UnSection.HaveSections;
        }
    }
}