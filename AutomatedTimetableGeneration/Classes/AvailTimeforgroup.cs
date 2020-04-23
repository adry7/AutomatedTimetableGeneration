using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class AvailTimeforgroup
    {
        public int Groupid { get; set; }
        public Availslotsday []Week { get; set; }

        public AvailTimeforgroup(int groupid)
        {
            Groupid = groupid;
            Week = new Availslotsday[6];
            for(int i =0;i<6;i++)
            {
                Week[i] = new Availslotsday(i);
            }
        }
    }
}