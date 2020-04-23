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

namespace AutomatedTimetableGeneration.Controllers
{
    public class DoctorController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: Doctor
        public ActionResult Show()
        {
            var docId = User.Identity.GetUserId();
            var courses = (from c in db.Courses
                           join doc in db.LinkDoctorCourses on c.ID equals doc.Course_id
                           where doc.Doctor_id == docId
                           select c).ToList();
            var time = db.Doctor_Available_Time.Include(g => g.Course);
           List< Doctor_Available_Time> list = new List< Doctor_Available_Time>();

           foreach(var i in time)
            {
                for (var j=0; j<courses.Count;j++)
                {
                    if (courses[j] == i.Course)
                        list.Add(i);
                        
                }
            }
            return View(list.ToList());
        }
            public ActionResult Index()
        {
            List<Doctor_Available_Time> Doctor_Available_Time = new List<Doctor_Available_Time>();
            for (int i = 0; i < 98; i++)
            {
                Doctor_Available_Time.Add(new Doctor_Available_Time { ID = 0, Course_id = 0, StartHour = 0, DayOfWeek = 0 });
            }
            var docId = User.Identity.GetUserId();
            var courses = (from c in db.Courses
                           join doc in db.LinkDoctorCourses on c.ID equals doc.Course_id
                           where doc.Doctor_id == docId
                           select c).ToList();
            ViewBag.Doctor_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Course_id = new SelectList(courses, "ID", "Name");
            ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            ViewBag.course__id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
            return View(Doctor_Available_Time);
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<Doctor_Available_Time> Doctor_Available_Time)
        {
            Doctor_Available_Time doctor_Available_Time = new Doctor_Available_Time();


            //var docId = (from c in db.AspNetUsers
            //             where
            //             c.Email == s
            //             select c.Id).Single();

            var docId = User.Identity.GetUserId();

            if (ModelState.IsValid) // m7tgen l dcotor id
            {
                foreach (var i in Doctor_Available_Time)
                {

                    if (i.DayOfWeek != 0)
                        db.Doctor_Available_Time.Add(i);
                }
                db.SaveChanges();
                // ModelState.Clear();

                Doctor_Available_Time = new List<Doctor_Available_Time>();
                for (int i = 0; i < 98; i++)
                {
                    Doctor_Available_Time.Add(new Doctor_Available_Time { ID = 0, Course_id = 0, StartHour = 0, DayOfWeek = 0 });
                }
            }
            var courses = (from c in db.Courses
                           join doc in db.LinkDoctorCourses on c.ID equals doc.Course_id
                           where doc.Doctor_id == docId
                           select c).ToList();

            //   ViewBag.Doctor_Id = new SelectList(db.AspNetUsers, "Id", "Email", doctor_Available_Time.Doctor_Id);
            ViewBag.Doctor_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Course_id = new SelectList(courses, "ID", "Name");
            ViewBag.course_hour = db.Courses.OrderBy(x => x.ID).Select(x => x.Hours).ToArray();
            ViewBag.course_name = db.Courses.OrderBy(x => x.ID).Select(x => x.Name).ToArray();

            ViewBag.course__id = db.Courses.OrderBy(x => x.ID).Select(x => x.ID).ToArray();
            return View(Doctor_Available_Time);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor_Available_Time doctor_Available_Time = db.Doctor_Available_Time.Find(id);
            if (doctor_Available_Time == null)
            {
                return HttpNotFound();
            }
            return View(doctor_Available_Time);
        }

        // POST: Doctor_Available_Time/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor_Available_Time doctor_Available_Time = db.Doctor_Available_Time.Find(id);
            db.Doctor_Available_Time.Remove(doctor_Available_Time);
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
    }
}
