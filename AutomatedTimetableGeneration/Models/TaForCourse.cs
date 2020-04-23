using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TaForCourse
    {
        public string ta_id;
        public int course_id;
        public TaForCourse(string t , int c)
        {
            this.ta_id = t;
            this.course_id = c;
        }
    }
}