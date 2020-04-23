using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class Slot
    {
        
        public int Start { get; set; }
        public int End { get; set; }
        public int RoomId { get; set; }

        public Slot(int start, int end, int roomId)
        {
            Start = start;
            End = end;
            RoomId = roomId;
        }

        public Slot(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}