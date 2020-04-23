using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class RoomDay
    {
        public int ID { get; set; }
        public bool [] Slots { get; set; }

        public RoomDay(int iD)
        {
            ID = iD;
            Slots = new bool[12];
            for(int i = 0; i < 12; i++) { Slots[i] = false; }
        }

        public void ReserveInterval(int start, int end)
        {

            for (int i = start; i <= end; i++)
            {
                Slots[i] = true;
            }
        }
        public void Remove(int start, int end)
        {

            for (int i = start; i <= end; i++)
            {
                Slots[i] = false;
            }
        }


    }
}