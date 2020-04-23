using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedTimetableGeneration.Models;
using WebApplication5.Models;

namespace AutomatedTimetableGeneration.Controllers
{
    public class taWishes1Controller : Controller
    {
        // GET: taWishes1
        public ActionResult Index()
        {
            MainAlgo();

            return View();
        }
        public static void TaForEachCourse(List<ta> sortedTAList, List<course> courseList, List<TaForCourse> taForCourse)
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            // wish 1
            for (int i = 0; i < sortedTAList.Count(); i++)
            {

                for (int j = 0; j < courseList.Count(); j++)
                {
                    if (courseList[j].id == sortedTAList[i].wish1)
                    {
                        // lw hours elcourse > hours el ta
                        if (courseList[j].remain_hours > sortedTAList[i].remain_hours)
                        {
                            if (courseList[j].course_hours != 0 && sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                                    sortedTAList[i].remain_hours++;
                            }
                            if (courseList[j].lab_hours != 0 && sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                                    sortedTAList[i].remain_hours++;
                            }


                            int x = sortedTAList[i].remain_hours;

                            //  sortedTAList[i].remain_hours = courseList[j].remain_hours - x;
                            courseList[j].remain_hours = courseList[j].remain_hours - x;
                            taForCourse.Add(new TaForCourse(sortedTAList[i].id, courseList[j].id));
                            // insert record in linkDoctorCourse
                            if (x != 0)
                            {
                                LinkDoctorCourse link = new LinkDoctorCourse();
                                link.Course_id = courseList[j].id;
                                link.Doctor_id = sortedTAList[i].id;
                                link.hours = x;
                                db.LinkDoctorCourses.Add(link);
                                db.SaveChanges();
                            }
                            // lw elta hours b2t zero aw 22l yshelo mn ellist 
                            sortedTAList.Remove(sortedTAList[i]);
                            if (i > 0 && i != sortedTAList.Count() - 1)
                                i--;
                            else if (i == 0)
                                i = 0;

                            if (courseList[j].remain_hours <= 0)
                            {
                                courseList.Remove(courseList[j]);
                                if (j > 0 && j != courseList.Count() - 1)
                                    j--;
                                else if (j == 0)
                                    j = 0;
                            }
                            break;


                        }
                        // lw hours ellecture <= hours el ta
                        else
                        {
                            if (courseList[j].course_hours != 0 && sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                                    sortedTAList[i].remain_hours--;
                            }
                            if (courseList[j].lab_hours != 0 && sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                                    sortedTAList[i].remain_hours--;
                            }

                            int x = courseList[j].remain_hours;
                            sortedTAList[i].remain_hours = sortedTAList[i].remain_hours - courseList[j].remain_hours;
                            // courseList[j].remain_hours = courseList[j].remain_hours - x;
                            taForCourse.Add(new TaForCourse(sortedTAList[i].id, courseList[j].id));
                            // insert record in linkDoctorCourse
                            if (x != 0)
                            {
                                LinkDoctorCourse link = new LinkDoctorCourse();
                                link.Course_id = courseList[j].id;
                                link.Doctor_id = sortedTAList[i].id;
                                link.hours = x;
                                db.LinkDoctorCourses.Add(link);
                                db.SaveChanges();
                            }

                            if (sortedTAList[i].remain_hours <= 0)
                            {
                                sortedTAList.Remove(sortedTAList[i]);
                                if (i > 0 && i != sortedTAList.Count() - 1)
                                    i--;
                                else if (i == 0)
                                    i = 0;
                            }


                            courseList.Remove(courseList[j]);
                            if (j > 0 && i != courseList.Count() - 1)
                                j--;
                            else if (j == 0)
                                j = 0;


                        }
                    }
                }

            }
            //wish2
            for (int i = 0; i < sortedTAList.Count(); i++)
            {

                for (int j = 0; j < courseList.Count(); j++)
                {
                    if (courseList[j].id == sortedTAList[i].wish2)
                    {
                        // lw hours elcourse > hours el ta
                        if (courseList[j].remain_hours > sortedTAList[i].remain_hours)
                        {
                            if (courseList[j].course_hours != 0 && sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                                    sortedTAList[i].remain_hours++;
                            }
                            if (courseList[j].lab_hours != 0 && sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                                    sortedTAList[i].remain_hours++;
                            }

                            int x = sortedTAList[i].remain_hours;

                            //sortedTAList[i].remain_hours = courseList[j].remain_hours - x;
                            courseList[j].remain_hours = courseList[j].remain_hours - x;
                            taForCourse.Add(new TaForCourse(sortedTAList[i].id, courseList[j].id));
                            // insert record in linkDoctorCourse
                            if (x != 0)
                            {
                                LinkDoctorCourse link = new LinkDoctorCourse();
                                link.Course_id = courseList[j].id;
                                link.Doctor_id = sortedTAList[i].id;
                                link.hours = x;
                                db.LinkDoctorCourses.Add(link);
                                db.SaveChanges();
                            }
                            // lw elta hours b2t zero aw 22l yshelo mn ellist 
                            sortedTAList.Remove(sortedTAList[i]);
                            if (i > 0 && i != sortedTAList.Count() - 1)
                                i--;
                            else if (i == 0)
                                i = 0;

                            if (courseList[j].remain_hours <= 0)
                            {
                                courseList.Remove(courseList[j]);
                                if (j > 0 && j != courseList.Count() - 1)
                                    j--;
                                else if (j == 0)
                                    j = 0;
                            }
                            break;


                        }
                        // lw hours ellecture <= hours el ta
                        else
                        {
                            if (courseList[j].course_hours != 0 && sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                                    sortedTAList[i].remain_hours--;
                            }
                            if (courseList[j].lab_hours != 0 && sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                                    sortedTAList[i].remain_hours--;
                            }

                            int x = courseList[j].remain_hours;
                            sortedTAList[i].remain_hours = sortedTAList[i].remain_hours - courseList[j].remain_hours;
                            //  courseList[j].remain_hours = courseList[j].remain_hours - x;
                            taForCourse.Add(new TaForCourse(sortedTAList[i].id, courseList[j].id));
                            // insert record in linkDoctorCourse
                            if (x != 0)
                            {
                                LinkDoctorCourse link = new LinkDoctorCourse();
                                link.Course_id = courseList[j].id;
                                link.Doctor_id = sortedTAList[i].id;
                                link.hours = x;
                                db.LinkDoctorCourses.Add(link);
                                db.SaveChanges();
                            }

                            if (sortedTAList[i].remain_hours <= 0)
                            {
                                sortedTAList.Remove(sortedTAList[i]);
                                if (i > 0 && i != sortedTAList.Count() - 1)
                                    i--;
                                else if (i == 0)
                                    i = 0;
                            }


                            courseList.Remove(courseList[j]);
                            if (j > 0 && i != courseList.Count() - 1)
                                j--;
                            else if (j == 0)
                                j = 0;


                        }
                    }
                }

            }
            // wish 3 
            //for (int i = 0; i < sortedTAList.Count(); i++)
            //{

            //    for (int j = 0; j < courseList.Count(); j++)
            //    {
            //        if (courseList[j].id == sortedTAList[i].wish3)
            //        {
            //            if (courseList[j].remain_hours > sortedTAList[i].remain_hours)
            //            {
            //                if (courseList[j].course_hours != 0 && sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
            //                {
            //                    while (sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
            //                        sortedTAList[i].remain_hours++;
            //                }
            //                else if (courseList[j].lab_hours != 0 && sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
            //                {
            //                    while (sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
            //                        sortedTAList[i].remain_hours++;
            //                }

            //                int x = sortedTAList[i].remain_hours;
            //                sortedTAList[i].remain_hours = x - sortedTAList[i].remain_hours;
            //                taForCourse.Add(new TaForCourse(sortedTAList[i].id, courseList[j].id));
            //                // insert record in linkDoctorCourse
            //                LinkDoctorCourse link = new LinkDoctorCourse();
            //                link.Course_id = courseList[j].id;
            //                link.Doctor_id = sortedTAList[i].id;
            //                link.hours = x;
            //                db.LinkDoctorCourses.Add(link);
            //                db.SaveChanges();
            //                // lw elta hours b2t zero aw 22l yshelo mn ellist 
            //                if (sortedTAList[i].remain_hours <= 0)
            //                {
            //                    sortedTAList.Remove(sortedTAList[i]);
            //                    if (i > 0)
            //                        i--;
            //                    else
            //                        i = 0;
            //                }
            //                courseList[j].remain_hours = courseList[j].remain_hours - x;
            //                if (courseList[j].remain_hours == 0)
            //                {
            //                    courseList.Remove(courseList[j]);
            //                    if (j > 0)
            //                        j--;
            //                    else
            //                        j = 0;
            //                }

            //                break;
            //            }
            //            else
            //            {
            //                int x = courseList[j].remain_hours;
            //                sortedTAList[i].remain_hours = courseList[j].remain_hours - sortedTAList[i].remain_hours;
            //                courseList[j].remain_hours = courseList[j].remain_hours - x;
            //                // insert record in linkDoctorCourse
            //                LinkDoctorCourse link = new LinkDoctorCourse();
            //                link.Course_id = courseList[j].id;
            //                link.Doctor_id = sortedTAList[i].id;
            //                link.hours = x;
            //                db.LinkDoctorCourses.Add(link);
            //                db.SaveChanges();

            //                if (sortedTAList[i].remain_hours <= 0)
            //                {
            //                    sortedTAList.Remove(sortedTAList[i]);
            //                    if (i > 0)
            //                        i--;
            //                    else
            //                        i = 0;
            //                }
            //                courseList[j].remain_hours = courseList[j].remain_hours - x;
            //                if (courseList[j].remain_hours == 0)
            //                {
            //                    courseList.Remove(courseList[j]);
            //                    if (j > 0)
            //                        j--;
            //                    else
            //                        j = 0;
            //                }

            //            }
            //        }
            //    }

            //}
            for (int i = 0; i < sortedTAList.Count(); i++)
            {

                for (int j = 0; j < courseList.Count(); j++)
                {
                    if (courseList[j].id == sortedTAList[i].wish3)
                    {
                        // lw hours elcourse > hours el ta
                        if (courseList[j].remain_hours > sortedTAList[i].remain_hours)
                        {
                            if (courseList[j].course_hours != 0 && sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                                    sortedTAList[i].remain_hours++;
                            }
                            if (courseList[j].lab_hours != 0 && sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                                    sortedTAList[i].remain_hours++;
                            }

                            int x = sortedTAList[i].remain_hours;

                            // sortedTAList[i].remain_hours = courseList[j].remain_hours - x;
                            courseList[j].remain_hours = courseList[j].remain_hours - x;
                            taForCourse.Add(new TaForCourse(sortedTAList[i].id, courseList[j].id));
                            // insert record in linkDoctorCourse
                            LinkDoctorCourse link = new LinkDoctorCourse();
                            link.Course_id = courseList[j].id;
                            link.Doctor_id = sortedTAList[i].id;
                            link.hours = x;
                            db.LinkDoctorCourses.Add(link);
                            db.SaveChanges();
                            // lw elta hours b2t zero aw 22l yshelo mn ellist 
                            sortedTAList.Remove(sortedTAList[i]);
                            if (i > 0 && i != sortedTAList.Count() - 1)
                                i--;
                            else if (i == 0)
                                i = 0;

                            if (courseList[j].remain_hours <= 0)
                            {
                                courseList.Remove(courseList[j]);
                                if (j > 0 && j != courseList.Count() - 1)
                                    j--;
                                else if (j == 0)
                                    j = 0;
                            }
                            break;


                        }
                        // lw hours ellecture <= hours el ta
                        else
                        {
                            if (courseList[j].course_hours != 0 && sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].course_hours != 0)
                                    sortedTAList[i].remain_hours--;
                            }
                            if (courseList[j].lab_hours != 0 && sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                            {
                                while (sortedTAList[i].remain_hours % courseList[j].lab_hours != 0)
                                    sortedTAList[i].remain_hours--;
                            }

                            int x = courseList[j].remain_hours;
                            sortedTAList[i].remain_hours = sortedTAList[i].remain_hours - courseList[j].remain_hours;
                            courseList[j].remain_hours = courseList[j].remain_hours - x;
                            taForCourse.Add(new TaForCourse(sortedTAList[i].id, courseList[j].id));
                            // insert record in linkDoctorCourse
                            if (x != 0)
                            {
                                LinkDoctorCourse link = new LinkDoctorCourse();
                                link.Course_id = courseList[j].id;
                                link.Doctor_id = sortedTAList[i].id;
                                link.hours = x;
                                db.LinkDoctorCourses.Add(link);
                                db.SaveChanges();
                            }

                            if (sortedTAList[i].remain_hours <= 0)
                            {
                                sortedTAList.Remove(sortedTAList[i]);
                                if (i > 0 && i != sortedTAList.Count() - 1)
                                    i--;
                                else if (i == 0)
                                    i = 0;
                            }


                            courseList.Remove(courseList[j]);
                            if (j > 0 && i != courseList.Count() - 1)
                                j--;
                            else if (j == 0)
                                j = 0;


                        }
                    }
                }

            }
        }



        public static void MainAlgo()
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            // Declare lists 
            List<course> courseList = new List<course>();
            List<ta> taList = new List<ta>();
            List<TaForCourse> taForCourse = new List<TaForCourse>();
            // load data from database in course List 
            var courseListdb = (from x in db.Courses
                                select new { x.ID, x.LabHours, x.SectionHours, x.AcademicYear_id }).ToList();
            for (int i = 0; i < courseListdb.Count(); i++)
            {
                if (courseListdb[i].LabHours != null || courseListdb[i].SectionHours != null)
                {
                    int courseHour = Convert.ToInt32(courseListdb[i].SectionHours);
                    int labHour = Convert.ToInt32(courseListdb[i].LabHours);
                    int total = totalHoursForCourse(courseListdb[i].AcademicYear_id, courseHour, labHour);
                    courseList.Add(new course((Convert.ToInt32(courseListdb[i].ID)), courseHour, total, labHour));
                }
            }

            // load data from database in ta list 
            var taInfo = (from x in db.AspNetUsers
                          where x.type == "Ta"
                          select new { x.Id, x.Experience }).ToList();

            var tawishesList = (from x in db.Ta_Wishes
                                select new { x.Ta_Id, x.Course_Id, x.Priority }).ToList();

            for (int i = 0; i < taInfo.Count(); i++)
            {
                int wish1 = 0;
                int wish2 = 0;
                for (int j = 0; j < tawishesList.Count(); j++)
                {

                    string ta_info_id = Convert.ToString(taInfo[i].Id);
                    string ta_wish_id = Convert.ToString(tawishesList[j].Ta_Id);
                    int ta_wish_pri = Convert.ToInt32(tawishesList[j].Priority);
                    if (ta_info_id == ta_wish_id)
                    {
                        if (ta_wish_pri == 1)
                        {
                            wish1 = Convert.ToInt32(tawishesList[j].Course_Id);

                        }
                        else if (ta_wish_pri == 2)
                        {
                            //if (wish1 == 0)
                            //{
                            //    j = 0;
                            //    continue;
                            //}
                            wish2 = Convert.ToInt32(tawishesList[j].Course_Id);

                        }
                        else if (ta_wish_pri == 3)
                        {
                            //if (wish2 == 0)
                            //{
                            //    j = 0; 
                            //    continue;
                            //}
                            taList.Add(new ta(tawishesList[j].Ta_Id, wish1, wish2, Convert.ToInt32(tawishesList[j].Course_Id), totalHoursForEachTa(), (Convert.ToInt32(taInfo[i].Experience))));
                            break;
                        }
                    }
                    else
                        continue;

                }
            }

            // choose victim from list 
            List<ta> victimList = new List<ta>();
            List<ta> nonVictimList = new List<ta>();
            List<string> temp = Victims();

            for (int i = 0; i < taList.Count(); i++)
            {
                bool check = false;
                for (int j = 0; j < temp.Count(); j++)
                {

                    if (temp[j] == taList[i].id)
                    {

                        victimList.Add(new ta(taList[i].id, taList[i].wish1, taList[i].wish2, taList[i].wish3, taList[i].remain_hours, taList[i].experiance));
                        check = true;
                        break;

                    }
                }
                if (check == false)
                {
                    nonVictimList.Add(new ta(taList[i].id, taList[i].wish1, taList[i].wish2, taList[i].wish3, taList[i].remain_hours, taList[i].experiance));
                }

            }
            // sort taList by experiance 
            List<ta> sortedTAList = new List<ta>();
            sortedTAList = sortList(victimList);
            // algorithm elawl ll victim 
            TaForEachCourse(sortedTAList, courseList, taForCourse);
            // algorithm l sana 4 
            List<ta> remain_ta = new List<ta>();
            List<course> remain_course = new List<course>();
            for (int i = 0; i < sortedTAList.Count(); i++)
            {
                remain_ta.Add(new ta(sortedTAList[i].id, sortedTAList[i].wish1, sortedTAList[i].wish2, sortedTAList[i].wish3, sortedTAList[i].remain_hours, sortedTAList[i].experiance));
            }
            sortedTAList = new List<ta>();
            sortedTAList = sortList(nonVictimList);
            // choose mwad sana 4
            var academic_4_id = (from x in db.AcademicYears
                                 where x.AcademicYear1 == 4
                                 select x.ID).SingleOrDefault();
            int acad_4 = Convert.ToInt32(academic_4_id);
            var academic_4 = (from x in db.Courses
                              where x.AcademicYear_id == acad_4
                              select x.ID).ToList();
            List<int> academic_list = new List<int>();
            for (int i = 0; i < academic_4.Count(); i++)
            {
                academic_list.Add(academic_4[i]);
            }
            List<course> courses_4 = new List<course>();
            List<course> coursesNot_4 = new List<course>();

            for (int i = 0; i < courseList.Count(); i++)
            {
                bool check = false;
                for (int j = 0; j < academic_list.Count(); j++)
                {

                    if (academic_list[j] == courseList[i].id)
                    {
                        courses_4.Add(new course(courseList[i].id, courseList[i].course_hours, courseList[i].remain_hours, courseList[i].lab_hours));

                        check = true;
                        break;

                    }
                }
                if (check == false)
                {
                    coursesNot_4.Add(new course(courseList[i].id, courseList[i].course_hours, courseList[i].remain_hours, courseList[i].lab_hours));

                }

            }

            TaForEachCourse(sortedTAList, courses_4, taForCourse);
            for (int i = 0; i < courses_4.Count(); i++)
            {
                remain_course.Add(new course(courses_4[i].id, courses_4[i].course_hours, courses_4[i].remain_hours, courses_4[i].lab_hours));
            }
            TaForEachCourse(sortedTAList, coursesNot_4, taForCourse);
            for (int i = 0; i < sortedTAList.Count(); i++)
            {
                remain_ta.Add(new ta(sortedTAList[i].id, sortedTAList[i].wish1, sortedTAList[i].wish2, sortedTAList[i].wish3, sortedTAList[i].remain_hours, sortedTAList[i].experiance));
            }

            for (int i = 0; i < coursesNot_4.Count(); i++)
            {
                remain_course.Add(new course(coursesNot_4[i].id, coursesNot_4[i].course_hours, coursesNot_4[i].remain_hours, coursesNot_4[i].lab_hours));
            }
            // elyear ely ana feha 3shan a add elvictim
            var years = (from x in db.Years
                         select x.ID).ToList();
            List<int> yearss = new List<int>();
            int year;
            if (years.Count() == 1)
            {
                year = 1;
            }
            else
            {
                for (int i = 0; i < years.Count(); i++)
                {
                    yearss.Add(years[i]);
                }

                year = yearss[years.Count() - 1];
            }

            var victim = (from x in db.VictimHistories
                          where x.Year_id == year
                          select x.Ta_id).ToList();
            for (int i = 0; i < remain_ta.Count(); i++)
            {
                VictimHistory victim1 = new VictimHistory();
                victim1.Ta_id = remain_ta[i].id;
                victim1.Year_id = year;
                db.VictimHistories.Add(victim1);
                db.SaveChanges();


            }
            remain_ta = sortList(remain_ta);
            // lw remain ta = remain course 
            for (int i = 0; i < remain_ta.Count(); i++)
            {
                for (int j = 0; j < remain_course.Count(); j++)
                {
                    if (remain_ta[i].remain_hours == remain_course[j].remain_hours)
                    {
                        if (remain_ta[i].remain_hours != 0)
                        {

                            LinkDoctorCourse link = new LinkDoctorCourse();
                            link.Course_id = remain_course[j].id;
                            link.Doctor_id = remain_ta[i].id;
                            link.hours = remain_ta[i].remain_hours;

                            db.LinkDoctorCourses.Add(link);
                            db.SaveChanges();
                        }
                        remain_ta.Remove(remain_ta[i]);
                        if (i > 0 && i != remain_ta.Count() - 1)
                            i--;
                        else if (i == 0)
                            i = 0;
                        remain_course.Remove(remain_course[j]);
                        if (j > 0 && j != remain_course.Count() - 1)
                            j--;
                        else if (j == 0)
                            j = 0;
                    }
                }
            }
            // lw remain ta >< remain course 
            for (int i = 0; i < remain_ta.Count(); i++)
            {
                for (int j = 0; j < remain_course.Count(); j++)
                {
                    if (remain_ta[i].remain_hours > remain_course[j].remain_hours)
                    {

                        if (remain_course[j].course_hours != 0 && remain_ta[i].remain_hours % remain_course[j].course_hours != 0)
                        {
                            while (remain_ta[i].remain_hours % remain_course[j].course_hours != 0)
                                remain_ta[i].remain_hours--;
                        }
                        if (remain_course[j].lab_hours != 0 && remain_ta[i].remain_hours % remain_course[j].lab_hours != 0)
                        {
                            while (remain_ta[i].remain_hours % remain_course[j].lab_hours != 0)
                                remain_ta[i].remain_hours--;
                        }
                        if (remain_course[j].remain_hours != 0)
                        {
                            LinkDoctorCourse link = new LinkDoctorCourse();
                            link.Course_id = remain_course[j].id;
                            link.Doctor_id = remain_ta[i].id;
                            link.hours = remain_course[j].remain_hours;
                            db.LinkDoctorCourses.Add(link);
                            db.SaveChanges();
                        }
                        remain_ta[i].remain_hours -= remain_course[j].remain_hours;
                        remain_course.Remove(remain_course[j]);
                        if (j > 0 && j != remain_course.Count() - 1)
                            j--;
                        else if (j == 0)
                            j = 0;

                    }
                    // lw course hours > ta hours 
                    else
                    {
                        if (remain_course[j].course_hours != 0 && remain_ta[i].remain_hours % remain_course[j].course_hours != 0)
                        {
                            while (remain_ta[i].remain_hours % remain_course[j].course_hours != 0)
                                remain_ta[i].remain_hours++;
                        }
                        if (remain_course[j].lab_hours != 0 && remain_ta[i].remain_hours % remain_course[j].lab_hours != 0)
                        {
                            while (remain_ta[i].remain_hours % remain_course[j].lab_hours != 0)
                                remain_ta[i].remain_hours++;
                        }
                        if (remain_ta[i].remain_hours != 0)
                        {
                            LinkDoctorCourse link = new LinkDoctorCourse();
                            link.Course_id = remain_course[j].id;
                            link.Doctor_id = remain_ta[i].id;
                            link.hours = remain_ta[i].remain_hours;
                            db.LinkDoctorCourses.Add(link);
                            db.SaveChanges();
                        }
                        remain_course[j].remain_hours -= remain_ta[i].remain_hours;
                        remain_ta.Remove(remain_ta[i]);
                        if (i > 0 && i != remain_ta.Count() - 1)
                            i--;
                        else if (i == 0)
                            i = 0;

                    }
                }
            }
        }

        public static int totalHoursForCourse(int acadmicYear_id, int courseHour, int labHour)
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            var sectionsCount = (from x in db.AcademicYears
                                 where x.ID == acadmicYear_id
                                 select x.SectionCount).Single();
            int totalHours = Convert.ToInt32(sectionsCount) * (courseHour + (labHour * 2));


            return totalHours;
        }
        public static int totalHoursForEachTa()
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            // total Hours for Courses 
            var courseInfo = (from x in db.Courses
                              select new { x.AcademicYear_id, x.SectionHours, x.LabHours }).ToList();
            int courseTotal = 0;
            for (int i = 0; i < courseInfo.Count; i++)
            {
                int courseHour = Convert.ToInt32(courseInfo[i].SectionHours);
                int labHour = Convert.ToInt32(courseInfo[i].LabHours);
                courseTotal += totalHoursForCourse((Convert.ToInt32(courseInfo[i].AcademicYear_id)), courseHour, labHour);
            }
            // tAs Number
            var taList = (from x in db.AspNetUsers
                          where x.type == "Ta"
                          select x.Id);

            int taCount = taList.Count();
            while (courseTotal % taCount != 0)
                courseTotal++;
            int avg_ForEachTa = courseTotal / taCount;
            return avg_ForEachTa;
        }
        public static List<ta> sortList(List<ta> unsortedList)
        {
            int i = 0, j = 1;
            int counter = 1;
            ta x = new ta("", 0, 0, 0, 0, 0);
            ta y = new ta("", 0, 0, 0, 0, 0);



            while (counter < unsortedList.Count())
            {

                x = unsortedList[i];
                y = unsortedList[j];
                if (x.experiance < y.experiance)
                {
                    Swap(ref x, ref y);
                    i = 0;
                    j = 1;
                    counter = 1;
                }
                else
                {
                    i++;
                    j++;
                    counter++;
                }


            }


            return unsortedList;
        }
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        // rturn list of string b IDs elvictims
        static List<string> Victims()
        {
            CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();
            List<string> victims = new List<string>();
            // bgeb id elyear w elsemseter ely fat 
            var years = (from x in db.Years
                         select x.ID).ToList();
            List<int> yearss = new List<int>();
            int lastyear;
            if (years.Count() == 1)
            {
                return victims;
            }
            else
            {
                for (int i = 0; i < years.Count(); i++)
                {
                    yearss.Add(years[i]);
                }

                lastyear = yearss[years.Count() - 2];
            }
            // hgeb victim elterm ely fat 
            var victim = (from x in db.VictimHistories
                          where x.Year_id == lastyear
                          select x.Ta_id).ToList();

            for (int i = 0; i < victim.Count(); i++)
            {
                victims.Add(victim[i]);
            }

            return victims;
        }
    }
}