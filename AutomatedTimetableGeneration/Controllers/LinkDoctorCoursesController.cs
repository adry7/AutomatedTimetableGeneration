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
    public class LinkDoctorCoursesController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: LinkDoctorCourses
        public ActionResult Index()
        {
            var linkDoctorCourses = db.LinkDoctorCourses.Include(l => l.AspNetUser).Include(l => l.Course);
            return View(linkDoctorCourses.ToList());
        }

        // GET: LinkDoctorCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkDoctorCourse linkDoctorCourse = db.LinkDoctorCourses.Find(id);
            if (linkDoctorCourse == null)
            {
                return HttpNotFound();
            }
            return View(linkDoctorCourse);
        }

        // GET: LinkDoctorCourses/Create
        public ActionResult Create()
        {
            ViewBag.Doctor_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name");
            return View();
        }

        // POST: LinkDoctorCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Doctor_id,Course_id,hours")] LinkDoctorCourse linkDoctorCourse)
        {
            if (ModelState.IsValid)
            {
                db.LinkDoctorCourses.Add(linkDoctorCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Doctor_id = new SelectList(db.AspNetUsers, "Id", "Email", linkDoctorCourse.Doctor_id);
            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name", linkDoctorCourse.Course_id);
            return View(linkDoctorCourse);
        }

        // GET: LinkDoctorCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkDoctorCourse linkDoctorCourse = db.LinkDoctorCourses.Find(id);
            if (linkDoctorCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.Doctor_id = new SelectList(db.AspNetUsers, "Id", "Email", linkDoctorCourse.Doctor_id);
            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name", linkDoctorCourse.Course_id);
            return View(linkDoctorCourse);
        }

        // POST: LinkDoctorCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Doctor_id,Course_id,hours")] LinkDoctorCourse linkDoctorCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkDoctorCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Doctor_id = new SelectList(db.AspNetUsers, "Id", "Email", linkDoctorCourse.Doctor_id);
            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name", linkDoctorCourse.Course_id);
            return View(linkDoctorCourse);
        }

        // GET: LinkDoctorCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkDoctorCourse linkDoctorCourse = db.LinkDoctorCourses.Find(id);
            if (linkDoctorCourse == null)
            {
                return HttpNotFound();
            }
            return View(linkDoctorCourse);
        }

        // POST: LinkDoctorCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LinkDoctorCourse linkDoctorCourse = db.LinkDoctorCourses.Find(id);
            db.LinkDoctorCourses.Remove(linkDoctorCourse);
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
