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
    public class TAController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: TA
        public ActionResult Index()
        {
            var TAId = User.Identity.GetUserId();
            var wishes = (from w in db.Ta_Wishes where w.Ta_Id == TAId select w).ToList();

            //var ta_Wishes = db.Ta_Wishes.Include(t => t.AspNetUser).Include(t => t.Course).Include(t => t.Year).Where((t =>t.Ta_Id)==TAId);
            return View(wishes.ToList());
        }

       

        // GET: TA/Create
        public ActionResult Create()
        {
            List<Ta_Wishes> wishes = new List<Ta_Wishes>();
            for (int i = 0; i < 3; i++)
            {
                wishes.Add(new Ta_Wishes());
            }

            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1");

            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name");

            return View(wishes);
        }

        // POST: TA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<Ta_Wishes> ta_Wishes)
        {
            Ta_Wishes TA = new Ta_Wishes();
            var TAId = User.Identity.GetUserId();
            // ta_Wishes.Ta_Id = TAId;

            if (ModelState.IsValid)
            {
                foreach (var i in ta_Wishes)
                {
                    i.Ta_Id = TAId;


                    if (i.Priority != 0)
                        db.Ta_Wishes.Add(i);
                }

                db.SaveChanges();

                return View(ta_Wishes);
            }
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", TA.Year_id);
            // ViewBag.Course_Id2 = new SelectList(db.Courses, "ID", "Name", TA.Course_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name", TA.Course_Id);


            return View(ta_Wishes);
        }

        // GET: TA/Edit/5
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
            // ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Wishes.Ta_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name", ta_Wishes.Course_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Wishes.Year_id);
            return View(ta_Wishes);
        }

        // POST: TA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Course_Id,Priority,Ta_Id,isAccepted,Year_id")] Ta_Wishes ta_Wishes)
        {
            var TAId = User.Identity.GetUserId();

            ta_Wishes.Ta_Id = TAId;
            if (ModelState.IsValid)
            {
                db.Entry(ta_Wishes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Ta_Id = new SelectList(db.AspNetUsers, "Id", "Email", ta_Wishes.Ta_Id);
            ViewBag.Course_Id = new SelectList(db.Courses, "ID", "Name", ta_Wishes.Course_Id);
            ViewBag.Year_id = new SelectList(db.Years, "ID", "Year1", ta_Wishes.Year_id);
            return View(ta_Wishes);
        }

        // GET: TA/Delete/5
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

        // POST: TA/Delete/5
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
