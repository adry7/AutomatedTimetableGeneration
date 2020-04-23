using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class Availslotsday
    {
        public int ID { get; set; }
        public List<int> AvailableTimes { get; set; }

        public Availslotsday(int iD)
        {
            ID = iD;
            AvailableTimes = new List<int>();
        }
       //this function for the initialization 
        //public void AddSlot(int start,int end)
        //{
        //    AvailableTimes.Add(new Slot(start, end));
        //}
        
    }
}