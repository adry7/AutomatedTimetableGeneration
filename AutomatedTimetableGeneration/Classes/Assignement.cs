using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class Assignement
    {

        public string CourseName { get; set; }
        public int Course_id { get; set; }
        public int GrpId { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public int Day { get; set; }
        public int RoomId { get; set; }
        public int AcademicYear_id { get; set; }
        public List<string> Doc_ids { get; set; }

        public Assignement(string courseName, int grpId, int academicYear_id, int day, int startHour, int endHour, int roomId,int course_id, List<string> doc_ids)
        {
            CourseName = courseName;
            Course_id = course_id;
            GrpId = grpId;
            StartHour = startHour;
            EndHour = endHour;
            Day = day;
            RoomId = roomId;
            AcademicYear_id = academicYear_id;
            Doc_ids = doc_ids;
        }


        //public Assignement(string courseName, int grpId, int academicYear_id, int day, int startHour, int endHour, int roomId, int course_id)
        //{
        //    CourseName = courseName;
        //    GrpId = grpId;
        //    AcademicYear_id = academicYear_id;
        //    RoomId = roomId;

        //    StartHour = startHour;
        //    Day = day;
        //    EndHour = endHour;
        //    Course_id = course_id;
        //}

        //public Assignement(string courseName, int grpId, int academicYear)
        //{
        //    CourseName = courseName;
        //    GrpId = grpId;
        //    AcademicYear_id = academicYear;



        //}
    }
}