using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class SectionDay
    {
        public int Id { get; set; }

        public bool isFreeDay { get; set; }
        public int Gap { get; set; }
        public bool[] Slots { get; set; }
        public int NumfHours { get; set; }

        public SectionDay(int id)
        {
            this.Id = id;
            this.NumfHours = 0;
           this.Slots = new bool[12];
            for (int i = 0; i < 12; i++) { this.Slots[i] = false; }
           this.Gap = 0;
            this.isFreeDay = true;
        }
        public void Addslots(int start,int end)
        {
           
            this.isFreeDay = false;
            for (int i = start; i <= end; i++) { this.Slots[i] = true; }
            GetNoHours();
        }
        public void Removeslots(int start,int end)
        {
           
            this.isFreeDay = false;
            for (int i = start; i <= end; i++) { this.Slots[i] = false; }
            
            for(int i=0;i<12;i++)
            {
                if (this.Slots[i] == true)
                {
                    this.isFreeDay = false;
                    GetNoHours();
                    break;
                }
               
                GetNoHours();
            }

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
               this.Gap = helpercounter;
        }

        public void GetNoHours()
        {
            int start = -1;
            int end = -1;

            for (int i = 0; i < 12; i++)
            {
                if (this.Slots[i] == true)
                {
                    start = i;

                    break;
                }
            }
            for (int i = 11; i >= 0; i--)
            {
                if (this.Slots[i] == true)
                {

                    end = i;

                    break;
                }
            }

            if (start == -1 && end == -1)
            {
               this.NumfHours = 0;
                this.Gap = 0;
            }
            else if (start == end)
            {
                this.NumfHours = 1;
                this.Gap = 0;
            }
            else
            {
                this.NumfHours = end - start + 1;
                this.GetGap(start, end);
            }
           
            if (NumfHours > 0)
                this.isFreeDay = false;
            else
                this.isFreeDay = true;
            
        }
    }
}