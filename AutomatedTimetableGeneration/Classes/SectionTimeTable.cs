using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class SectionTimeTable
    {
        public int Sectionid { get; set; }
        public int Groupid { get; set; }
        public SectionDay [] Week { get; set; }
        public int AcademicYear { get; set; }

        public SectionTimeTable(int  groupid,  int academicYear,int sectionid)
        {
            this.Sectionid = sectionid;
           this.Groupid = groupid;
           this.AcademicYear = academicYear;
           this.Week = new SectionDay[6];
            for (int i = 0; i < 6; i++)
            { 
                this.Week[i] = new SectionDay(i);
            }
        }

        public int FreeDaysCount()
        {
            int freedays = 0;
            for (int i = 0; i < Week.Length; i++)
            {
                if (Week[i].isFreeDay)
                    freedays++;
            }
            return freedays;
        }
    }
}