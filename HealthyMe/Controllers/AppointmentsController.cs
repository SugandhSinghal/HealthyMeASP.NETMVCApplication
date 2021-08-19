using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthyMe.Models;

namespace HealthyMe.Controllers
{
    public class AppointmentsController : Controller
    {
        private HeathyMe_Entities db = new HeathyMe_Entities();

        // GET: Appointments
        public ActionResult Index()
        {

            if (User.IsInRole("Client"))
            {
                // This code help to set the user name who log in 
                var userName = User.Identity.Name;
                var appointmentList = db.Appointments.Where(u => u.ClientName == userName).ToList();
                return View(appointmentList);
            }
            else
            {
                return View(db.Appointments.ToList());
            }
            
        }

        public ActionResult Calendar()
        {
            // This code is to get the Calender
            return View(db.Appointments.ToList());
        }



        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
       
        [Authorize(Roles = "Client")]
        public ActionResult Create()
        {
            // this code is to set the client name when creating appointment
            Appointment appointment = new Appointment();
            string currentUserName = User.Identity.Name;
            appointment.ClientName = currentUserName;
            return View(appointment);
          //  var context = new HeathyMe_Entities();
          //  var trainerName = (from t in context.Profiles select t.Name).ToList();
          //  ViewBag.TrainerNames = new SelectList(trainerName);
           // return View();
        }


       
        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,ClientName,TrainerName,Date,AppointmentAddress")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,ClientName,TrainerName,Date,AppointmentAddress")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

       


        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
