using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomatedTimetableGeneration.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace AutomatedTimetableGeneration.Controllers
{
    public class UsersController : Controller
    {
        private CollegeDatabaseEntities10 db = new CollegeDatabaseEntities10();

        // GET: AspNetUsers
        public ActionResult IndexTA()
        {
          //  var aspNetUsers = db.AspNetUsers.Include(a => a.Department);

            //   var TAs = Roles.GetUsersInRole("TA").Select(Membership.GetUser).ToList();
            var TAs = db.AspNetRoles.Where(x => x.Name == "TA").ToList();
           
          //  ViewBag.TA = new SelectList(TAs[0].AspNetUsers);

            return View(TAs[0].AspNetUsers);
        }
        public ActionResult IndexDoc()
        {
            //var aspNetUsers = db.AspNetUsers.Include(a => a.Department);

            var Docs = db.AspNetRoles.Where(x => x.Name == "Doctor").ToList();


            return View(Docs[0].AspNetUsers);
        }


        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

      

        //// GET: AspNetUsers/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUser aspNetUser = db.AspNetUsers.Find(id);
        //    if (aspNetUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUser);
        //}

        //// POST: AspNetUsers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    AspNetUser aspNetUser = db.AspNetUsers.Find(id);
        //    db.AspNetUsers.Remove(aspNetUser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
