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
    public class Ta_WishesController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: Ta_Wishes
        public ActionResult Index()
        {
            var ta_Wishes = db.Ta_Wishes.Include(t => t.AspNetUser).Include(t => t.Course);
            return View(ta_Wishes.ToList());
        }

        // GET: Ta_Wishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ta_Wishes ta_Wishes = db.Ta_Wishes.Find(id);
            if (ta_Wishes == null)
            {
                return HttpNotFound();
            }
            return View(ta_Wishes);
        }

        // GET: Ta_Wishes/Create
        public ActionResult Create()
        {
            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name");
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1");
            return View();
        }

        // POST: Ta_Wishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Course_Id,Priority,Ta_Id,isAccepted,Year_id")] Ta_Wishes ta_Wishes)
        {
            if (ModelState.IsValid)
            {
                db.Ta_Wishes.Add(ta_Wishes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Wishes.Ta_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name", ta_Wishes.Course_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Wishes.Year_id);
            return View(ta_Wishes);
        }

        // GET: Ta_Wishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ta_Wishes ta_Wishes = db.Ta_Wishes.Find(id);
            if (ta_Wishes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Wishes.Ta_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name", ta_Wishes.Course_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Wishes.Year_id);
            return View(ta_Wishes);
        }

        // POST: Ta_Wishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Course_Id,Priority,Ta_Id,isAccepted,Year_id")] Ta_Wishes ta_Wishes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ta_Wishes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Wishes.Ta_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name", ta_Wishes.Course_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Wishes.Year_id);
            return View(ta_Wishes);
        }

        // GET: Ta_Wishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ta_Wishes ta_Wishes = db.Ta_Wishes.Find(id);
            if (ta_Wishes == null)
            {
                return HttpNotFound();
            }
            return View(ta_Wishes);
        }

        // POST: Ta_Wishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ta_Wishes ta_Wishes = db.Ta_Wishes.Find(id);
            db.Ta_Wishes.Remove(ta_Wishes);
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
