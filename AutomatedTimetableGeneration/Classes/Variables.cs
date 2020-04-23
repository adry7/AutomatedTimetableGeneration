using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class Variables
    {
        public List<Room> Rooms { get; set; }
        public int GroupId { get; set; }
        // public CSPModel csp { get; set; }
        public Course course { get; set; }
        public List<Doctor_Available_Time> DocAvailTime { get; set; }
        public List<string> Doc_ids { get; set; }

        public Variables(List<Room> rooms, int groupId, Course course, List<Doctor_Available_Time> docAvailTime, List<string> doc_ids)
        {
            Rooms = rooms;
            GroupId = groupId;
            this.course = course;
            DocAvailTime = docAvailTime;
            Doc_ids = doc_ids;
        }


        //public string DocName { get; set; }
        //public bool? isExternal { get; set; }
        //public int GroupCount { get; set; }
        //public int AcademicYear_id { get; set; }

        //public Variables(List<Room> rooms, int groupId, Course course, List<Doctor_Available_Time> docAvailTime /*int groupCount, int academicYear_id*/)
        //{
        //    Rooms = rooms;
        //    GroupId = groupId;
        //    this.course = course;
        //    DocAvailTime = docAvailTime;
        //    //GroupCount = groupCount;
        //    //AcademicYear_id = academicYear_id;
        //}
        ////public Variables(List<Room> rooms, int groupId, CSPModel csp)
        ////{
        ////    Rooms = rooms;
        ////    GroupId = groupId;
        ////    this.csp = csp;
        ////}


    }
}