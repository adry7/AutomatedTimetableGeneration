using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomatedTimetableGeneration.Models;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;

namespace AutomatedTimetableGeneration.Controllers
{
    public class HomeController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: Home
        //public ActionResult profile()
        //{
        //    //AspNetUser  Aspuser =new
        //    //    (from c in db.AspNetUsers
        //    //            where c.Id == User.Identity.GetUserId()
        //    //            select c).ToList();
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "The generation done successfully";
            CollegeDatabaseEntities10 _db = new CollegeDatabaseEntities10();

            var del = (from x in db.LectureTimes
                       select x);
            db.LectureTimes.RemoveRange(del);

            var dell = (from x in db.SectionTimes
                       select x);
            db.SectionTimes.RemoveRange(dell);
            // var res = functions.Generatelecandsec()
            List<GroupModel> GrpList = new List<GroupModel>();
            functions func = new functions();
            var res = func.Generate_lect( ref GrpList);
           var assin = func.generatesecs(GrpList);
            var lecTimeTable = new LectureTime();
            foreach (var item in res)
            {
                var crs_id = _db.Courses.Where(x => x.Name == item.CourseName).Select(x => x.ID).Single();
                lecTimeTable = new LectureTime() { Course_id = crs_id, Room_id = item.RoomId, DayOfWeek = item.Day, Grp_id = item.GrpId, StartHour = item.StartHour };
                _db.LectureTimes.Add(lecTimeTable);
                _db.SaveChanges();
                Console.WriteLine("{0} : {1} : {2}", item.CourseName, item.StartHour, item.Day.ToString());

            }
            foreach (var item in assin)
            {

                if (item.RoomId !=-1)
                 
                { 
                SectionTime x = new SectionTime();
                x.Course_id = item.CourseId;
                x.Section_id = item.SectionId;
                x.Room_id = item.RoomId;
                x.DayOfWeek = item.Day;
                x.StartHour = item.Start;
                x.isLab = item.IsLab;
                x.isSection = item.IsSection;
                   // if(item.Tasids)
                    x.Ta_id = item.Tasids[0];
                _db.SectionTimes.Add(x);
                _db.SaveChanges(); }
             }


            return View();
        }
        public ActionResult charts()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Index()
        {
            //if(User.Identity.IsAuthenticated)
            //    return RedirectToAction("Profile1");
            //else
            //    return RedirectToAction("Login","Account");
            return View();
        }
        public ActionResult TAChart()
        {
            int fillData = 0;
            int  non_fill = 0;
            TaEnterData(fillData, non_fill);
            new Chart(width: 600, height: 600)
                .AddSeries(
                chartType: "column",
                xValue: new[] { "Enter the data", "Not Enter the data" },
                yValues: new[] { fillData, non_fill }).Write("png");
            return null;
        }

        // GET: Home/Details/5
        public ActionResult Profile1()
        {
           
            AspNetUser aspNetUser = db.AspNetUsers.Find(User.Identity.GetUserId());
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: Home/Create
       
        // GET: Home/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department_id = new SelectList(db.Departments, "ID", "Name", aspNetUser.Department);
            return View(aspNetUser);
        }
        

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Department_id,Experience,isExternal")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            ViewBag.Department_id = new SelectList(db.Departments, "ID", "Name", aspNetUser.Department);
            return View(aspNetUser);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public static void TaEnterData(int enter , int not_enter)
        {
            CollegeDatabaseEntities10 db1 = new CollegeDatabaseEntities10();
            var ta_list = (from x in db1.AspNetUsers
                           where x.type == "Ta"
                           select x.Id).ToList();
            var wishes = (from x in db1.Ta_Wishes
                          select x.Ta_Id).ToList();
            enter = 0;
             not_enter = 0;
            for(int i =0; i < ta_list.Count(); i++)
            {
                bool check = false;
                for (int j = 0; j < wishes.Count(); j++)
                {
                    if (ta_list[i] == wishes[j])
                    {
                        check = true;
                        enter++;
                        break;
                    }
                    else
                        continue;
                }
                if (check == false)
                    not_enter++;
            }
         
        }
       
        public ActionResult taChart()
        {
            int not_enter = Not_EnterDataTa();
            int enter = EnterDataForTa();


          
            new Chart(width: 350, height: 350)
                .AddTitle("Teaching Assistants Statistics")
               .AddSeries(
               chartType: "pie",
               xValue: new[] { "number of TAs that choose thier wishes ", "Not Yet" },
               yValues: new[] { enter, not_enter }).Write("png");
            return null;
        }
        public ActionResult hallsChart()
        {
            List<KeyValuePair<string, int>> final = new List<KeyValuePair<string, int>>();
            final = HallsData();


            new Chart(width: 350, height: 350)
                .AddTitle("Halls Statistics")
               .AddSeries(
               chartType: "column",
                xValue: new[] { final[0].Key, final[1].Key, final[2].Key },
               yValues: new[] { final[0].Value, final[1].Value, final[2].Value }).Write("png");
            
            return null;
        }

        public ActionResult CourseChart()
        {
            int not_enter = Not_EnterDataCourse();
            int enter = EnterDataForCourser();
            string mytheme =
                @"<Chart BackColor=""Transparent"" >
<ChartAreas>
<ChartArea Name = ""Default"" BackColor= ""Transparent""></ChartArea>
</ChartAreas>
</Chart>";
            new Chart(width: 350, height: 350)
                .AddTitle("Course Statistics")
                .AddSeries(
                chartType: "pie",
                xValue: new[] { "Courses Have times  ", "Not Yet" },
                yValues: new[] { enter, not_enter }).Write("png");
            return null;
        }
        public static int EnterDataForTa()
        {
            CollegeDatabaseEntities10 db1 = new CollegeDatabaseEntities10();
            var ta_list = (from x in db1.AspNetUsers
                           where x.type == "Ta"
                           select x.Id).ToList();
            var wishes = (from x in db1.Ta_Wishes
                          select x.Ta_Id).ToList();
            int enter = 0;



            for (int i = 0; i < ta_list.Count(); i++)
            {
                bool check = false;
                for (int j = 0; j < wishes.Count(); j++)
                {
                    if (ta_list[i] == wishes[j])
                    {
                        check = true;
                        enter++;
                        break;
                    }
                    else
                        continue;
                }

            }
            return enter;
        }
        public static int Not_EnterDataTa()
        {
            CollegeDatabaseEntities10 db1 = new CollegeDatabaseEntities10();
            var ta_list = (from x in db1.AspNetUsers
                           where x.type == "Ta"
                           select x.Id).ToList();
            var wishes = (from x in db1.Ta_Wishes
                          select x.Ta_Id).ToList();
            int not_enter = 0;



            for (int i = 0; i < ta_list.Count(); i++)
            {
                bool check = false;
                for (int j = 0; j < wishes.Count(); j++)
                {
                    if (ta_list[i] == wishes[j])
                    {
                        check = true;
                        break;
                    }
                    else
                        continue;
                }
                if (check == false)
                    not_enter++;
            }
            return not_enter;
        }
        public static int EnterDataForCourser()
        {
            CollegeDatabaseEntities10 db1 = new CollegeDatabaseEntities10();
            var course_list = (from x in db1.Courses
                               select x.ID).ToList();
            var Ava_time = (from x in db1.Doctor_Available_Time
                            select x.Course_id).ToList();
            int enter = 0;
            for (int i = 0; i < course_list.Count(); i++)
            {

                for (int j = 0; j < Ava_time.Count(); j++)
                {
                    if (course_list[i] == Ava_time[j])
                    {

                        enter++;
                        break;
                    }
                    else
                        continue;
                }

            }
            return enter;
        }
        public static int Not_EnterDataCourse()
        {
            CollegeDatabaseEntities10 db1 = new CollegeDatabaseEntities10();
            var course_list = (from x in db1.Courses

                               select x.ID).ToList();
            var avai_time = (from x in db1.Doctor_Available_Time
                             select x.Course_id).ToList();
            int not_enter = 0;



            for (int i = 0; i < course_list.Count(); i++)
            {
                bool check = false;
                for (int j = 0; j < avai_time.Count(); j++)
                {
                    if (course_list[i] == avai_time[j])
                    {
                        check = true;
                        break;
                    }
                    else
                        continue;
                }
                if (check == false)
                    not_enter++;
            }
            return not_enter;
        }
        public static List<KeyValuePair<string, int>> HallsData()
        {
            List<KeyValuePair<string, int>> final = new List<KeyValuePair<string, int>>();
            CollegeDatabaseEntities10 db1 = new CollegeDatabaseEntities10();
            var rooms_list = (from x in db1.Rooms
                              where x.RoomType_id == 1
                              select new { x.ID, x.Name }).ToList();
            var lecture_rooms = (from x in db1.LectureTimes
                                 select x.Room_id).ToList();




            for (int i = 0; i < rooms_list.Count(); i++)
            {
                int counter = 0;
                for (int j = 0; j < lecture_rooms.Count(); j++)
                {
                    if (rooms_list[i].ID == lecture_rooms[j])
                    {
                        counter++;
                    }

                }
                if (counter != 0)
                    final.Add(new KeyValuePair<string, int>(rooms_list[i].Name, counter));
            }
            return final;
        }
    }
}
