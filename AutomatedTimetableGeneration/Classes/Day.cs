using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class Day
    {
        public int Id { get; set; }

        public bool isFreeDay { get; set; }
        public int Gap { get; set; }
        public bool[] Slots { get; set; }
        public int NumfHours { get; set; }
        public List<KeyValuePair<int, Slot>> CourseStartEnd { get; set; }  // key is course name  , value is start and end time

        public Day(int id)
        {
            this.Id = id;
            NumfHours = 0;
            Slots = new bool[12];
            for (int i = 0; i < 12; i++) { Slots[i] = false; }
            Gap = 0;
            CourseStartEnd = new List<KeyValuePair<int, Slot>>();
            isFreeDay = true;
        }
        public void addinterval(int course_id, int roomid, int start, int end)
        {
            int from = start;
            int to = end;
            CourseStartEnd.Add(new KeyValuePair<int, Slot>(course_id, new Slot(start, end, roomid)));
            for (int i = from - 8; i < to - 8; i++) { Slots[i] = true; }
            if (CourseStartEnd.Count() > 0)
            {
                isFreeDay = false;
            }
            GetNoHours();
        }

        public void Removeinterval(int course_id)
        {
            var temp = CourseStartEnd.Where(x => x.Key == course_id).Single();
            for (int i = temp.Value.Start - 8; i < temp.Value.End - 8; i++)
            {
                Slots[i] = false;
            }
            CourseStartEnd.Remove(temp);

            if (CourseStartEnd.Count() == 0)
            {
                isFreeDay = true;
            }
            else
                isFreeDay = false;
            GetNoHours();
        }
        private void GetGap(int start, int end)
        {
            int basecounter = 0;
            int helpercounter = -1;
            for (int i = start; i <= end; i++)
            {
                if (Slots[i] == false)
                    basecounter++;
                else
                {
                    if (helpercounter < basecounter)
                    {
                        helpercounter = basecounter;
                        basecounter = 0;
                    }
                    else
                    {
                        basecounter = 0;
                    }
                }
            }
            if (basecounter > helpercounter) { Gap = basecounter; }
            else
                Gap = helpercounter;
        }

        public void GetNoHours()
        {
            int start = -1;
            int end = -1;

            for (int i = 0; i < 12; i++)
            {
                if (Slots[i] == true)
                {
                    start = i;

                    break;
                }
            }
            for (int i = 11; i >= 0; i--)
            {
                if (Slots[i] == true)
                {

                    end = i;

                    break;
                }
            }

            //NumfHours = end - start;
            //GetGap(start, end);
            if(start==-1&&end==-1)
            {
                NumfHours = 0;
                Gap = 0;
            }
            else if (start == end)
            {
                NumfHours = 1;
                Gap = 0;
            }
            else
            {
                NumfHours = end - start + 1;
                GetGap(start, end);
            }

            if (NumfHours > 0)
                isFreeDay = false;
            else
                isFreeDay = true;
        }



    }
}