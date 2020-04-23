using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class RoomsTimeTables
    {
        public int Roomid { get; set; }
        public RoomDay [] Week { get; set; }
        public int RoomTypeid  { get; set; }
        public RoomsTimeTables(int roomid,int roomtid)
        {
            Roomid = roomid;
            RoomTypeid = roomtid;
            Week = new RoomDay[6];
            for(int i = 0; i < 6; i++)
            {
                Week[i] = new RoomDay(i);
            }
        }

        
    }
}