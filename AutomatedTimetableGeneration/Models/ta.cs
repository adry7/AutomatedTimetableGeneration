using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ta
    {
       public  string id { set; get; }
        public int wish1 { set; get; }
        public int wish2 { set; get; }
        public int wish3 { set; get; }
        public int remain_hours { set; get; }
        public int experiance  { set; get; }
        public ta(string i, int w1, int w2, int w3, int r, int e)
        {
           this.id  = i;
            this.wish1 = w1;
            this.wish2 = w2;
            this.wish3 = w3;
            this.remain_hours = r;
            this.experiance = e;
        }
    }
   
}