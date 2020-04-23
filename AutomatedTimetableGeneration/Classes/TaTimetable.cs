using AutomatedTimetableGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Classes
{
    public class TaTimetable
    {
        public string Id { get; set; }
        public TaDay[] Week { get; set; }
        public List<KeyValuePair<int,int?>> Course_hours { get; set; }

        public TaTimetable(string id, List<KeyValuePair<int, int?>> course_hours)
        {
            Id = id;
            Course_hours = course_hours;
            Week = new TaDay[6];
            for (int i = 0; i < 6; i++)
            {
                Week[i] = new TaDay(i);
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