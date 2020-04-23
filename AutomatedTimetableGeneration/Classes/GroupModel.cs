using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class GroupModel
    {
        public int GrpId { get; set; }
        public string Name { get; set; }
        public int AcadmicYear_id { get; set; }
        public Day[] Week { get; set; }
       // public int Nofdays { get; set; }


        public GroupModel(int grpid, string name,int academicyear)
        {
            this.GrpId = grpid;
            this.Name = name;
            this.AcadmicYear_id = academicyear;
            Week = new Day[6];
            for (int i = 0; i < 6; i++)
            {
                //Week[i].Id = i;
                Week[i] = new Day(i);

            }

        }
        public int FreeDaysCount()
        {
            int freedays = 0;
            for(int i=0;i<Week.Length;i++)
            {
                if (Week[i].isFreeDay)
                    freedays++;
            }
            return freedays;
        }
    }
}
