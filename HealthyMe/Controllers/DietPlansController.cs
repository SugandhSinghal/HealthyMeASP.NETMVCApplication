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
    public class DietPlansController : Controller
    {
        private HeathyMe_Entities db = new HeathyMe_Entities();

        // GET: DietPlans
        public ActionResult Index()
        {
            // Here I have done role base authentication

            if (User.IsInRole("Client"))
            {
                var userName = User.Identity.Name;
                var dietList = db.DietPlans.Where(u => u.UserName == userName).ToList();
                return View(dietList);
            }
            else
            {
                return View(db.DietPlans.ToList());
            }
            
        }

        // GET: DietPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietPlan dietPlan = db.DietPlans.Find(id);
            if (dietPlan == null)
            {
                return HttpNotFound();
            }
            return View(dietPlan);
        }

        // GET: DietPlans/Create

        [Authorize(Roles = "Trainer")]
       
        public ActionResult Create()
        {
            
            //var userName = User.Identity.Name;
            //from c in db.Appointments join r in db.Ratings on c.AppointmentId equals r.AppointmentId where c.ClientName == userNameselect r
            var userNameList = (from p in db.Profiles  select p.Name).ToList();
            ViewBag.UserName = new SelectList(userNameList);
            return View();
          
        }

        // POST: DietPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DietId,DietName,Accuracy,Level,DietDescription,UserName")] DietPlan dietPlan)
        {
            if (ModelState.IsValid)
            {
                db.DietPlans.Add(dietPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dietPlan);
        }

        // GET: DietPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietPlan dietPlan = db.DietPlans.Find(id);
            if (dietPlan == null)
            {
                return HttpNotFound();
            }
            return View(dietPlan);
        }

        // POST: DietPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DietId,DietName,Accuracy,Level,DietDescription,UserName")] DietPlan dietPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dietPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dietPlan);
        }

        // GET: DietPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietPlan dietPlan = db.DietPlans.Find(id);
            if (dietPlan == null)
            {
                return HttpNotFound();
            }
            return View(dietPlan);
        }

        // POST: DietPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DietPlan dietPlan = db.DietPlans.Find(id);
            db.DietPlans.Remove(dietPlan);
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
