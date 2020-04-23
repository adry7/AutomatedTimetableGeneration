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
    public class ConstraintOfTAController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: ConstraintOfTA
        public ActionResult Index()
        {
            var ta_Constraints = db.Ta_Constraints.Include(t => t.AspNetUser).Include(t => t.Year);
            return View(ta_Constraints.ToList());
        }

        // GET: ConstraintOfTA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ta_Constraints ta_Constraints = db.Ta_Constraints.Find(id);
            if (ta_Constraints == null)
            {
                return HttpNotFound();
            }
            return View(ta_Constraints);
        }

        // GET: ConstraintOfTA/Create
        public ActionResult Create()
        {
            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1");
            return View();
        }

        // POST: ConstraintOfTA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ta_Id,Start_Date,End_Date,Description,Year_id")] Ta_Constraints ta_Constraints)
        {
            var TAId = User.Identity.GetUserId();
          
                ta_Constraints.Ta_Id = TAId;
                db.Ta_Constraints.Add(ta_Constraints);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Constraints.Ta_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Constraints.Year_id);
            return View(ta_Constraints);
        }

        // GET: ConstraintOfTA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ta_Constraints ta_Constraints = db.Ta_Constraints.Find(id);
            if (ta_Constraints == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Constraints.Ta_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Constraints.Year_id);
            return View(ta_Constraints);
        }

        // POST: ConstraintOfTA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ta_Id,Start_Date,End_Date,Description,Year_id")] Ta_Constraints ta_Constraints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ta_Constraints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Constraints.Ta_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Constraints.Year_id);
            return View(ta_Constraints);
        }

        // GET: ConstraintOfTA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ta_Constraints ta_Constraints = db.Ta_Constraints.Find(id);
            if (ta_Constraints == null)
            {
                return HttpNotFound();
            }
            return View(ta_Constraints);
        }

        // POST: ConstraintOfTA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ta_Constraints ta_Constraints = db.Ta_Constraints.Find(id);
            db.Ta_Constraints.Remove(ta_Constraints);
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
