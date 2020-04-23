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
    public class AcademicYearsController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: AcademicYears
        public ActionResult Index()
        {
            var academicYears = db.AcademicYears.Include(a => a.Year);
            return View(academicYears.ToList());
        }

        // GET: AcademicYears/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicYear = db.AcademicYears.Find(id);
            if (academicYear == null)
            {
                return HttpNotFound();
            }
            return View(academicYear);
        }

        // GET: AcademicYears/Create
        public ActionResult Create()
        {
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1");
            return View();
        }

        // POST: AcademicYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Year_id,StudentsCount,GroupCount,SectionCount,AcademicYear1,DayWorkHours,AllowedGapHours")] AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                db.AcademicYears.Add(academicYear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", academicYear.Year_id);
            return View(academicYear);
        }

        // GET: AcademicYears/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicYear = db.AcademicYears.Find(id);
            if (academicYear == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", academicYear.Year_id);
            return View(academicYear);
        }

        // POST: AcademicYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Year_id,StudentsCount,GroupCount,SectionCount,AcademicYear1,DayWorkHours,AllowedGapHours")] AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicYear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", academicYear.Year_id);
            return View(academicYear);
        }

        // GET: AcademicYears/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicYear academicYear = db.AcademicYears.Find(id);
            if (academicYear == null)
            {
                return HttpNotFound();
            }
            return View(academicYear);
        }

        // POST: AcademicYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcademicYear academicYear = db.AcademicYears.Find(id);
            db.AcademicYears.Remove(academicYear);
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
