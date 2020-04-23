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
    public class VictimHistoriesController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: VictimHistories
        public ActionResult Index()
        {
            var victimHistories = db.VictimHistories.Include(v => v.AspNetUser).Include(v => v.Year);
            return View(victimHistories.ToList());
        }

        // GET: VictimHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VictimHistory victimHistory = db.VictimHistories.Find(id);
            if (victimHistory == null)
            {
                return HttpNotFound();
            }
            return View(victimHistory);
        }

        // GET: VictimHistories/Create
        public ActionResult Create()
        {
            ViewBag.Ta_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1");
            return View();
        }

        // POST: VictimHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Year_id,Ta_id,HoursPerWeek")] VictimHistory victimHistory)
        {
            if (ModelState.IsValid)
            {
                db.VictimHistories.Add(victimHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ta_id = new SelectList(db.AspNetUsers, "Id", "Email", victimHistory.Ta_id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", victimHistory.Year_id);
            return View(victimHistory);
        }

        // GET: VictimHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VictimHistory victimHistory = db.VictimHistories.Find(id);
            if (victimHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ta_id = new SelectList(db.AspNetUsers, "Id", "Email", victimHistory.Ta_id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", victimHistory.Year_id);
            return View(victimHistory);
        }

        // POST: VictimHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Year_id,Ta_id,HoursPerWeek")] VictimHistory victimHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(victimHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ta_id = new SelectList(db.AspNetUsers, "Id", "Email", victimHistory.Ta_id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", victimHistory.Year_id);
            return View(victimHistory);
        }

        // GET: VictimHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VictimHistory victimHistory = db.VictimHistories.Find(id);
            if (victimHistory == null)
            {
                return HttpNotFound();
            }
            return View(victimHistory);
        }

        // POST: VictimHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VictimHistory victimHistory = db.VictimHistories.Find(id);
            db.VictimHistories.Remove(victimHistory);
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
