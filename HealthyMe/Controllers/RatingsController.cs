using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthyMe.Models;
using Microsoft.AspNet.Identity;

namespace HealthyMe.Controllers
{
    public class RatingsController : Controller
    {
        private HeathyMe_Entities db = new HeathyMe_Entities();

        // GET: Ratings
        public ActionResult Index()
        {
            if (User.IsInRole("Client"))
            {
                var userName = User.Identity.GetUserName();
                var selectedRating = (from c in db.Appointments
                                 join r in db.Ratings on c.AppointmentId
                                 equals r.AppointmentId
                                 where c.ClientName == userName
                                 select r ).ToList();
                return View(selectedRating); 
            }
            else
            {
                return View(db.Ratings.ToList());
            }
        }



        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        // Only client can create ratings
        [Authorize(Roles = "Client")]
        public ActionResult Create()

        {
            var userName = User.Identity.Name;
            var appointmentId = (from c in db.Appointments where c.ClientName == userName select c.AppointmentId).ToList();
            ViewBag.AppointmentId = new SelectList(appointmentId);
           // ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "AppointmentId");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "RatingId,Rating1,AppointmentId,Comments")] Rating rating)
        {
            rating.AppointmentId = Convert.ToInt32(Request["AppointmentId"]);
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "AppointmentId", rating.AppointmentId);
            return View(rating);
        }

        // GET: Ratings/Edit/5s
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "ClientName", rating.AppointmentId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "RatingId,Rating1,AppointmentId,Comments")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "ClientName", rating.AppointmentId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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
