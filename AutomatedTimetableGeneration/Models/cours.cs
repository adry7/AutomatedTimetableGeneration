using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class course
    {
       public int id { set; get; }
        public int course_hours { set; get; }
        public int lab_hours { set; get; }
        public int remain_hours { set; get; }
        public course(int i , int c , int r  , int l)
            {
            id = i;
            course_hours = c;
            remain_hours = r;
            lab_hours = l;
           
}
    }
}