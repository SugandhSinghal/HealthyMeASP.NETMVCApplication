using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthyMe.Models;

namespace HealthyMe.Controllers
{
    // This controler is used to upload posters to the web application
    public class FileUploadsController : Controller
    {
        private fileUpload_Entity db = new fileUpload_Entity();

        // GET: FileUploads
        public ActionResult Index()
        {
            return View(db.FileUploads.ToList());
        }

        // GET: FileUploads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileUpload fileUpload = db.FileUploads.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        // GET: FileUploads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileUploads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Path")] FileUpload fileUpload, HttpPostedFileBase
postedFile
 )
        {
            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            fileUpload.Path = myUniqueFileName;
            TryValidateModel(fileUpload);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/UploadFile/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = fileUpload.Path + fileExtension;
                fileUpload.Path = filePath;
                postedFile.SaveAs(serverPath + fileUpload.Path);

                db.FileUploads.Add(fileUpload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileUpload);
        }

        // GET: FileUploads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileUpload fileUpload = db.FileUploads.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        // POST: FileUploads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Path")] FileUpload fileUpload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileUpload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileUpload);
        }

        // GET: FileUploads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileUpload fileUpload = db.FileUploads.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        // POST: FileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileUpload fileUpload = db.FileUploads.Find(id);
            db.FileUploads.Remove(fileUpload);
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
