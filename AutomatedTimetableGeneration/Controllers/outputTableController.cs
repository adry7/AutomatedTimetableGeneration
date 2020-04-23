using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedTimetableGeneration.Models;
namespace AutomatedTimetableGeneration.Controllers
{
    public class outputTableController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
        // GET: outputTable
        public ActionResult Index()
        {
            // lecture time  
            // lecture time  
            ViewBag.day = (from c in db.LectureTimes
                           select c.DayOfWeek).ToArray();
            // ViewBag.day = new SelectList(db.LectureTimes, "ID", "DayOfWeek");
            ViewBag.room_id_lecture = (from c in db.LectureTimes
                                       select c.Room_id).ToArray();

            ViewBag.course_id_lecture = (from c in db.LectureTimes
                                         select c.Course_id).ToArray();
            ViewBag.startHour = (from c in db.LectureTimes
                                 select c.StartHour).ToArray();
            ViewBag.group_id_lecture = (from c in db.LectureTimes
                                        select c.Grp_id).ToArray();
            // room
            /////
            List<Doctorsforcourses> Docsnames = new List<Doctorsforcourses>();
            List<int> coursesids = new List<int>();
            coursesids = (from c in db.LectureTimes
                          join v in db.Groups on c.Grp_id equals v.ID
                          where v.AcadmicYear_id == 2
                          select c.Course_id).ToList();
            var Docs = db.AspNetRoles.Where(x => x.Name == "Doctor").ToList();
            var Doctors = Docs[0].AspNetUsers;
            for (int i = 0; i < coursesids.Count; i++)
            {
                int id = coursesids[i];
                var query = (from x in db.LinkDoctorCourses
                             join S in db.AspNetUsers on x.Doctor_id equals S.Id
                             where (x.Course_id == id && S.type != "Ta")
                             select S.UserName).ToList();


                if (query.Count == 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), ""));
                }
                else if (query.Count > 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), query[1].ToString()));
                }

            }

           
            ViewBag.Doc1name = (from c in Docsnames select c.Docname1).ToArray();
            ViewBag.Doc2name = (from c in Docsnames select c.Docname2).ToArray();

            ViewBag.room_id = db.Rooms.Where(x => x.RoomType_id == 1).Select(x => x.ID).ToArray();
            //(from roo in db.Rooms where roo.RoomType_id == 1 select new { roo.ID }).ToArray();
            //db.Rooms.Select(x => x.ID).ToArray();

            // var s = 
            ViewBag.room_name = db.Rooms.Where(x => x.RoomType_id == 1).Select(x => x.Name).ToArray();
            //db.Rooms.OrderBy(x => x.ID).Select(x => x.Name).ToArray();
            // Course 
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            ViewBag.course_id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
            ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
            //group 
            ViewBag.group_id = (from c in db.Groups
                                select c.ID).ToArray();
            ViewBag.group_name = db.Groups.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            return View();
        }


        public ActionResult LecYear1 ()
        {
           
                // lecture time  
                ViewBag.day = (from c in db.LectureTimes
                               join v in db.Groups on c.Grp_id equals v.ID
                               where v.AcadmicYear_id == 2
                               select c.DayOfWeek).ToArray();
            // ViewBag.day = new SelectList(db.LectureTimes, "ID", "DayOfWeek");
            ViewBag.room_id_lecture = (from c in db.LectureTimes
                                       join v in db.Groups on c.Grp_id equals v.ID
                                       where v.AcadmicYear_id == 2
                                       join r in db.Rooms on c.Room_id equals r.ID
                                       where r.RoomType_id == 1
                                       select c.Room_id).ToArray();

            ViewBag.course_id_lecture = (from c in db.LectureTimes
                                             join v in db.Groups on c.Grp_id equals v.ID
                                             where v.AcadmicYear_id == 2
                                             select c.Course_id).ToArray();
                ViewBag.startHour = (from c in db.LectureTimes
                                     join v in db.Groups on c.Grp_id equals v.ID
                                     where v.AcadmicYear_id == 2
                                     select c.StartHour).ToArray();
                ViewBag.group_id_lecture = (from c in db.LectureTimes
                                            join v in db.Groups on c.Grp_id equals v.ID
                                            where v.AcadmicYear_id == 2
                                            select c.Grp_id).ToArray();
                // room
                List<Doctorsforcourses> Docsnames = new List<Doctorsforcourses>();
                List<int> coursesids = new List<int>();
                coursesids = (from c in db.LectureTimes
                              join v in db.Groups on c.Grp_id equals v.ID
                              where v.AcadmicYear_id == 2
                              select c.Course_id).ToList();
            var Docs = db.AspNetRoles.Where(x => x.Name == "Doctor").ToList();
            var Doctors = Docs[0].AspNetUsers;
            for (int i = 0; i < coursesids.Count; i++)
            {
                int id = coursesids[i];
                var query = (from x in db.LinkDoctorCourses
                             join S in db.AspNetUsers on x.Doctor_id equals S.Id
                             where (x.Course_id == id && S.type != "Ta")
                             select S.UserName).ToList();


                if (query.Count == 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), ""));
                }
                else if (query.Count > 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), query[1].ToString()));
                }

            }

            ViewBag.Doc1name = (from c in Docsnames select c.Docname1).ToArray();
                ViewBag.Doc2name = (from c in Docsnames select c.Docname2).ToArray();

                ViewBag.room_id = db.Rooms.Where(x => x.RoomType_id == 1).Select(x => x.ID).ToArray();
                
            ViewBag.room_name = db.Rooms.Where(x => x.RoomType_id == 1).OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            // Course 
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

                ViewBag.course_id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
                ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
                //group 
                ViewBag.group_id = (from c in db.Groups
                                    select c.ID).ToArray();
                ViewBag.group_name = db.Groups.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

                return View();
            }

        public ActionResult LecYear2()
        {

            // lecture time  
            ViewBag.day = (from c in db.LectureTimes
                           join v in db.Groups on c.Grp_id equals v.ID
                           where v.AcadmicYear_id == 3
                           select c.DayOfWeek).ToArray();
            // ViewBag.day = new SelectList(db.LectureTimes, "ID", "DayOfWeek");
            ViewBag.room_id_lecture = (from c in db.LectureTimes
                                       join v in db.Groups on c.Grp_id equals v.ID
                                       where v.AcadmicYear_id == 3
                                       join r in db.Rooms on c.Room_id equals r.ID
                                       where r.RoomType_id == 1
                                       select c.Room_id).ToArray();

            ViewBag.course_id_lecture = (from c in db.LectureTimes
                                         join v in db.Groups on c.Grp_id equals v.ID
                                         where v.AcadmicYear_id == 3
                                         select c.Course_id).ToArray();
            ViewBag.startHour = (from c in db.LectureTimes
                                 join v in db.Groups on c.Grp_id equals v.ID
                                 where v.AcadmicYear_id == 3
                                 select c.StartHour).ToArray();
            ViewBag.group_id_lecture = (from c in db.LectureTimes
                                        join v in db.Groups on c.Grp_id equals v.ID
                                        where v.AcadmicYear_id == 3
                                        select c.Grp_id).ToArray();
            // room
            List<Doctorsforcourses> Docsnames = new List<Doctorsforcourses>();
            List<int> coursesids = new List<int>();
            coursesids = (from c in db.LectureTimes
                          join v in db.Groups on c.Grp_id equals v.ID
                          where v.AcadmicYear_id == 3
                          select c.Course_id).ToList();
            var Docs = db.AspNetRoles.Where(x => x.Name == "Doctor").ToList();
            var Doctors = Docs[0].AspNetUsers;
            for (int i = 0; i < coursesids.Count; i++)
            {
                int id = coursesids[i];
                var query = (from x in db.LinkDoctorCourses
                             join S in db.AspNetUsers on x.Doctor_id equals S.Id
                             where (x.Course_id == id && S.type != "Ta")
                             select S.UserName).ToList();


                if (query.Count == 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), ""));
                }
                else if (query.Count > 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), query[1].ToString()));
                }

            }

            ViewBag.Doc1name = (from c in Docsnames select c.Docname1).ToArray();
            ViewBag.Doc2name = (from c in Docsnames select c.Docname2).ToArray();

            ViewBag.room_id = db.Rooms.Where(x => x.RoomType_id == 1).Select(x => x.ID).ToArray();
            ;
            ViewBag.room_name = db.Rooms.Where(x => x.RoomType_id == 1).OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            // Course 
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            ViewBag.course_id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
            ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
            //group 
            ViewBag.group_id = (from c in db.Groups
                                select c.ID).ToArray();
            ViewBag.group_name = db.Groups.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            return View();
        }

        public ActionResult LecYear3()
        {

            // lecture time  
            ViewBag.day = (from c in db.LectureTimes
                           join v in db.Groups on c.Grp_id equals v.ID
                           where v.AcadmicYear_id == 4
                           select c.DayOfWeek).ToArray();
            // ViewBag.day = new SelectList(db.LectureTimes, "ID", "DayOfWeek");
            ViewBag.room_id_lecture = (from c in db.LectureTimes
                                       join v in db.Groups on c.Grp_id equals v.ID
                                       where v.AcadmicYear_id == 4
                                       join r in db.Rooms on c.Room_id equals r.ID
                                       where r.RoomType_id == 1
                                       select c.Room_id).ToArray();

            ViewBag.course_id_lecture = (from c in db.LectureTimes
                                         join v in db.Groups on c.Grp_id equals v.ID
                                         where v.AcadmicYear_id == 4
                                         select c.Course_id).ToArray();
            ViewBag.startHour = (from c in db.LectureTimes
                                 join v in db.Groups on c.Grp_id equals v.ID
                                 where v.AcadmicYear_id == 4
                                 select c.StartHour).ToArray();
            ViewBag.group_id_lecture = (from c in db.LectureTimes
                                        join v in db.Groups on c.Grp_id equals v.ID
                                        where v.AcadmicYear_id == 4
                                        select c.Grp_id).ToArray();
            // room
            List<Doctorsforcourses> Docsnames = new List<Doctorsforcourses>();
            List<int> coursesids = new List<int>();
            coursesids = (from c in db.LectureTimes
                          join v in db.Groups on c.Grp_id equals v.ID
                          where v.AcadmicYear_id == 4
                          select c.Course_id).ToList();
            var Docs = db.AspNetRoles.Where(x => x.Name == "Doctor").ToList();
            var Doctors = Docs[0].AspNetUsers;
            for (int i = 0; i < coursesids.Count; i++)
            {
                int id = coursesids[i];
                var query = (from x in db.LinkDoctorCourses
                             join S in db.AspNetUsers on x.Doctor_id equals S.Id
                             where (x.Course_id == id && S.type != "Ta")
                             select S.UserName).ToList();


                if (query.Count == 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), ""));
                }
                else if (query.Count > 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), query[1].ToString()));
                }

            }

            ViewBag.Doc1name = (from c in Docsnames select c.Docname1).ToArray();
            ViewBag.Doc2name = (from c in Docsnames select c.Docname2).ToArray();

            ViewBag.room_id = db.Rooms.Where(x => x.RoomType_id == 1).Select(x => x.ID).ToArray();
            ;
            ViewBag.room_name = db.Rooms.Where(x => x.RoomType_id == 1).OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            // Course 
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            ViewBag.course_id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
            ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
            //group 
            ViewBag.group_id = (from c in db.Groups
                                select c.ID).ToArray();
            ViewBag.group_name = db.Groups.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            return View();
        }

        public ActionResult LecYear4()
        {

            // lecture time  
            ViewBag.day = (from c in db.LectureTimes
                           join v in db.Groups on c.Grp_id equals v.ID
                           where v.AcadmicYear_id == 5
                           select c.DayOfWeek).ToArray();
            // ViewBag.day = new SelectList(db.LectureTimes, "ID", "DayOfWeek");
            ViewBag.room_id_lecture = (from c in db.LectureTimes
                                       join v in db.Groups on c.Grp_id equals v.ID
                                       where v.AcadmicYear_id == 5
                                       join r in db.Rooms on c.Room_id equals r.ID
                                       where r.RoomType_id == 1
                                       select c.Room_id).ToArray();

            ViewBag.course_id_lecture = (from c in db.LectureTimes
                                         join v in db.Groups on c.Grp_id equals v.ID
                                         where v.AcadmicYear_id == 5
                                         select c.Course_id).ToArray();
            ViewBag.startHour = (from c in db.LectureTimes
                                 join v in db.Groups on c.Grp_id equals v.ID
                                 where v.AcadmicYear_id == 5
                                 select c.StartHour).ToArray();
            ViewBag.group_id_lecture = (from c in db.LectureTimes
                                        join v in db.Groups on c.Grp_id equals v.ID
                                        where v.AcadmicYear_id == 5
                                        select c.Grp_id).ToArray();
            // room
            List<Doctorsforcourses> Docsnames = new List<Doctorsforcourses>();
            List<int> coursesids = new List<int>();
            coursesids = (from c in db.LectureTimes
                          join v in db.Groups on c.Grp_id equals v.ID
                          where v.AcadmicYear_id == 5
                          select c.Course_id).ToList();
            var Docs = db.AspNetRoles.Where(x => x.Name == "Doctor").ToList();
            var Doctors = Docs[0].AspNetUsers;
            for (int i = 0; i < coursesids.Count; i++)
            {
                int id = coursesids[i];
                var query = (from x in db.LinkDoctorCourses
                             join S in db.AspNetUsers on x.Doctor_id equals S.Id
                             where (x.Course_id == id && S.type != "Ta")
                             select S.UserName).ToList();


                if (query.Count == 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), ""));
                }
                else if (query.Count > 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), query[1].ToString()));
                }

            }

            ViewBag.Doc1name = (from c in Docsnames select c.Docname1).ToArray();
            ViewBag.Doc2name = (from c in Docsnames select c.Docname2).ToArray();

            ViewBag.room_id = db.Rooms.Where(x => x.RoomType_id == 1).Select(x => x.ID).ToArray();
            ;
            ViewBag.room_name = db.Rooms.Where(x => x.RoomType_id == 1).OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            // Course 
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            ViewBag.course_id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
            ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
            //group 
            ViewBag.group_id = (from c in db.Groups
                                select c.ID).ToArray();
            ViewBag.group_name = db.Groups.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            return View();
        }

        //public ActionResult SecYear1()
        //{

        //    // lecture time  
        //    ViewBag.day = (from c in db.SectionTimes
        //                   join v in db.Sections  on c.Section_id equals v.ID
        //                   join r in db.Groups on v.Grp_id equals r.ID
        //                   where r.AcadmicYear_id == 2
        //                   select c.DayOfWeek).ToArray();
        //    // ViewBag.day = new SelectList(db.LectureTimes, "ID", "DayOfWeek");
        //    ViewBag.room_id_lecture = (from c in db.SectionTimes
        //                               join v in db.Sections on c.Section_id equals v.ID
        //                               join g in db.Groups on v.Grp_id equals g.ID
        //                               where g.AcadmicYear_id == 2
                                   
        //                               //join r in db.Rooms on c.Room_id equals r.ID
        //                               //where r.RoomType_id != 1
        //                               select c.Room_id).ToArray();

        //    ViewBag.course_id_lecture = (from c in db.SectionTimes
        //                                 join v in db.Sections on c.Section_id equals v.ID
        //                                 join r in db.Groups on v.Grp_id equals r.ID
        //                                 where r.AcadmicYear_id == 2
        //                                 select c.Course_id).ToArray();
        //    ViewBag.startHour = (from c in db.SectionTimes
        //                         join v in db.Sections on c.Section_id equals v.ID
        //                         join r in db.Groups on v.Grp_id equals r.ID
        //                         where r.AcadmicYear_id == 2
        //                         select c.StartHour).ToArray();
        //    ViewBag.group_id_lecture = (from c in db.SectionTimes
        //                                join v in db.Sections on c.Section_id equals v.ID
        //                                join g in db.Groups on v.Grp_id equals g.ID
        //                                where g.AcadmicYear_id == 2
        //                                select v.Grp_id).ToArray();
        //    // room
        //    List<Doctorsforcourses> Docsnames = new List<Doctorsforcourses>();
        //    List<int> coursesids = new List<int>();
        //    coursesids = (from c in db.SectionTimes
        //                  join v in db.Sections on c.Section_id equals v.ID
        //                  join r in db.Groups on v.Grp_id equals r.ID
        //                  where r.AcadmicYear_id == 2
        //                  select c.Course_id).ToList();
        //    var Docs = db.AspNetRoles.Where(x => x.Name == "Doctor").ToList();
        //    var Doctors = Docs[0].AspNetUsers;
        //    for (int i = 0; i < coursesids.Count; i++)
        //    {
        //        int id = coursesids[i];
        //        var query = (from x in db.LinkDoctorCourses
        //                     join S in db.AspNetUsers on x.Doctor_id equals S.Id
        //                     where (x.Course_id == id && S.type == "Ta")
        //                     select S.UserName).ToList();


        //        if (query.Count == 1)
        //        {
        //            Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), ""));
        //        }
        //        else if (query.Count > 1)
        //        {
        //            Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), query[1].ToString()));
        //        }

        //    }

        //    ViewBag.Doc1name = (from c in Docsnames select c.Docname1).ToArray();
        //    ViewBag.Doc2name = (from c in Docsnames select c.Docname2).ToArray();

        //    ViewBag.room_id = db.Rooms.Where(x => x.RoomType_id != 1).Select(x => x.ID).ToArray();

        //    ViewBag.room_name = db.Rooms.Where(x => x.RoomType_id != 1).OrderBy(x => x.ID).Select(x => x.Name).ToArray();

        //    // Course 
        //    ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

        //    ViewBag.course_id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
        //    ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
        //    //group 
        //    ViewBag.group_id = (from c in db.Groups
        //                        select c.ID).ToArray();
        //    ViewBag.group_name = db.Groups.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

        //    return View();
        //}
        public ActionResult SecYear1()
        {
            // lecture time  
            // lecture time  
            ViewBag.day = (from c in db.SectionTimes
                           select c.DayOfWeek).ToArray();
            // ViewBag.day = new SelectList(db.LectureTimes, "ID", "DayOfWeek");
            ViewBag.room_id_lecture = (from c in db.SectionTimes
                                       select c.Room_id).ToArray();

            ViewBag.course_id_lecture = (from c in db.SectionTimes
                                         select c.Course_id).ToArray();
            ViewBag.startHour = (from c in db.SectionTimes
                                 select c.StartHour).ToArray();
            ViewBag.group_id_lecture = (from c in db.SectionTimes
                                        join s in db.Sections on c.Section_id equals s.ID
                                        
                                        select s.Grp_id).ToArray();



            ViewBag.TaName = (from c in db.AspNetUsers
                              join t in db.SectionTimes
                              on c.Id equals t.Ta_id
                              select c.UserName).ToList();

            var islsection = (from c in db.SectionTimes select c.isSection).ToList();
            var islab = (from b in db.SectionTimes select b.isLab).ToList();
            List<string> result = new List<string>();
            for(int i=0;i<islsection.Count;i++)
            {
                if (islsection[i]==true)
                    result.Add("section");
                if (islab[i] == true)
                    result.Add("lab");
            }
            var islsectionh = (from c in  db.Courses
                               join br in db.SectionTimes 
                               on c.ID equals br.Course_id
                               select c.SectionHours).ToList();

            var islabh = (from c in db.Courses
                          join br in db.SectionTimes
                          on c.ID equals br.Course_id
                          select c.LabHours).ToList();

            List<int> durations = new List<int>();
            for (int i = 0; i < islsectionh.Count; i++)
            {
                if (islsectionh[i] !=null)
                    durations.Add(islsectionh[i].Value);
                if (islabh[i] !=null)
                    durations.Add(islabh[i].Value);
            }
            ViewBag.Hours = durations.ToList();
            ViewBag.laborsection = result.ToList();
            ViewBag.sectionNum = (from c in db.SectionTimes
                                 join r in db.Sections on c.Section_id equals r.ID
                                  select r.Name).ToArray();
            /////
            List < Doctorsforcourses > Docsnames = new List<Doctorsforcourses>();
            List<int> coursesids = new List<int>();
            coursesids = (from c in db.SectionTimes
                         
                          select c.Course_id).ToList();
            var Docs = db.AspNetRoles.Where(x => x.Name == "TA").ToList();
            var Doctors = Docs[0].AspNetUsers;
            for (int i = 0; i < coursesids.Count; i++)
            {
                int id = coursesids[i];
                var query = (from x in db.LinkDoctorCourses
                             join S in db.AspNetUsers on x.Doctor_id equals S.Id
                             where (x.Course_id == id && S.type == "Ta")
                             select S.UserName).ToList();


                if (query.Count == 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), ""));
                }
                else if (query.Count > 1)
                {
                    Docsnames.Add(new Doctorsforcourses(coursesids[i], query[0].ToString(), query[1].ToString()));
                }

            }


            ViewBag.Doc1name = (from c in Docsnames select c.Docname1).ToArray();
            ViewBag.Doc2name = (from c in Docsnames select c.Docname2).ToArray();

            ViewBag.room_id = db.Rooms.Select(x => x.ID).ToArray();
            //(from roo in db.Rooms where roo.RoomType_id == 1 select new { roo.ID }).ToArray();
            //db.Rooms.Select(x => x.ID).ToArray();

            // var s = 
            ViewBag.room_name = db.Rooms.Select(x => x.Name).ToArray();
            //db.Rooms.OrderBy(x => x.ID).Select(x => x.Name).ToArray();
            // Course 
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            ViewBag.course_id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
            ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
           
            //group 
            ViewBag.group_id = (from c in db.Groups
                                select c.ID).ToArray();
            ViewBag.group_name = db.Groups.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            return View();
        }

    }
}