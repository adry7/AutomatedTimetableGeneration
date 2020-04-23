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
    public class Ta_ConstraintsController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: Ta_Constraints
        public ActionResult Index()
        {
            var ta_Constraints = db.Ta_Constraints.Include(t => t.AspNetUser).Include(t => t.Year);
            return View(ta_Constraints.ToList());
        }

        // GET: Ta_Constraints/Details/5
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

        // GET: Ta_Constraints/Create
      
        // GET: Ta_Constraints/Edit/5
       
        // GET: Ta_Constraints/Delete/5
        
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
