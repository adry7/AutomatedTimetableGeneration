using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTimetableGeneration.Models
{
    public class Doctorsforcourses
    {
        public int Courseid { get; set; }
        public string  Docname1 { get; set; }
        public string  Docname2 { get; set; }

        public Doctorsforcourses(int courseid, string docname1, string docname2)
        {
            Courseid = courseid;
            Docname1 = docname1;
            Docname2 = docname2;
        }
    }
}