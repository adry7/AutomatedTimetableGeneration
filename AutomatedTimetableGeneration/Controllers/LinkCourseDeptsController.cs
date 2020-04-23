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
    public class LinkCourseDeptsController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: LinkCourseDepts
        public ActionResult Index()
        {
            var linkCourseDepts = db.LinkCourseDepts.Include(l => l.Course).Include(l => l.Department);
            return View(linkCourseDepts.ToList());
        }

        // GET: LinkCourseDepts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkCourseDept linkCourseDept = db.LinkCourseDepts.Find(id);
            if (linkCourseDept == null)
            {
                return HttpNotFound();
            }
            return View(linkCourseDept);
        }

        // GET: LinkCourseDepts/Create
        public ActionResult Create()
        {
            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name");
            ViewBag.Department_id = new SelectList(db.Departments, "ID", "Name");
            return View();
        }

        // POST: LinkCourseDepts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Course_id,Department_id")] LinkCourseDept linkCourseDept)
        {
            if (ModelState.IsValid)
            {
                db.LinkCourseDepts.Add(linkCourseDept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name", linkCourseDept.Course_id);
            ViewBag.Department_id = new SelectList(db.Departments, "ID", "Name", linkCourseDept.Department_id);
            return View(linkCourseDept);
        }

        // GET: LinkCourseDepts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkCourseDept linkCourseDept = db.LinkCourseDepts.Find(id);
            if (linkCourseDept == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name", linkCourseDept.Course_id);
            ViewBag.Department_id = new SelectList(db.Departments, "ID", "Name", linkCourseDept.Department_id);
            return View(linkCourseDept);
        }

        // POST: LinkCourseDepts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Course_id,Department_id")] LinkCourseDept linkCourseDept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkCourseDept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course_id = new SelectList(db.Courses, "ID", "Name", linkCourseDept.Course_id);
            ViewBag.Department_id = new SelectList(db.Departments, "ID", "Name", linkCourseDept.Department_id);
            return View(linkCourseDept);
        }

        // GET: LinkCourseDepts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkCourseDept linkCourseDept = db.LinkCourseDepts.Find(id);
            if (linkCourseDept == null)
            {
                return HttpNotFound();
            }
            return View(linkCourseDept);
        }

        // POST: LinkCourseDepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LinkCourseDept linkCourseDept = db.LinkCourseDepts.Find(id);
            db.LinkCourseDepts.Remove(linkCourseDept);
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
