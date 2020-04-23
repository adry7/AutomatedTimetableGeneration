using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomatedTimetableGeneration.Models;

namespace AutomatedTimetableGeneration.Controllers
{
    public class CoursesController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.AcademicYear).Include(c => c.RoomType);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.AcademicYear_id = new SelectList(db.AcademicYears, "ID", "AcademicYear1");
            ViewBag.RoomType_id = new SelectList(db.RoomTypes, "ID", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Hours,HaveLab,HaveSection,LabHours,SectionHours,RoomType_id,AcademicYear_id,StaffCount")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicYear_id = new SelectList(db.AcademicYears, "ID", "AcademicYear1", course.AcademicYear_id);
            ViewBag.RoomType_id = new SelectList(db.RoomTypes, "ID", "Name", course.RoomType_id);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicYear_id = new SelectList(db.AcademicYears, "ID", "AcademicYear1", course.AcademicYear_id);
            ViewBag.RoomType_id = new SelectList(db.RoomTypes, "ID", "Name", course.RoomType_id);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Hours,HaveLab,HaveSection,LabHours,SectionHours,RoomType_id,AcademicYear_id,StaffCount")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicYear_id = new SelectList(db.AcademicYears, "ID", "AcademicYear1", course.AcademicYear_id);
            ViewBag.RoomType_id = new SelectList(db.RoomTypes, "ID", "Name", course.RoomType_id);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
