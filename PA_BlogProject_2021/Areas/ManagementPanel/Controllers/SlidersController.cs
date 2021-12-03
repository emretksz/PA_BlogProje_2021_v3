using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PA_BlogProject_2021.Areas.ManagementPanel.Helpers;
using PA_BlogProject_2021.Models;

namespace PA_BlogProject_2021.Areas.ManagementPanel.Controllers
{
    public class SlidersController : Controller
    {
        private BlogProjeContext db = new BlogProjeContext();

        // GET: ManagementPanel/Sliders
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        // GET: ManagementPanel/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // GET: ManagementPanel/Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SliderId,ImageURL,Title,SubTitle,IsActive")] Sliders sliders,HttpPostedFileBase imgFile )
        {
            if (ModelState.IsValid)
            {
                if (imgFile!=null)
                {
                    sliders.ImageURL = ImageUpload.SaveImage(imgFile,1800,1165);
                }
                db.Sliders.Add(sliders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sliders);
        }

        // GET: ManagementPanel/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // POST: ManagementPanel/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SliderId,ImageURL,Title,SubTitle,IsActive")] Sliders sliders,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                Sliders update = db.Sliders.Find(sliders.SliderId);

                if (imgFile!=null)
                {
                    ImageUpload.DeleteByPath(update.ImageURL);
                    update.ImageURL = ImageUpload.SaveImage(imgFile, 1800, 1165);
                }

                update.IsActive = sliders.IsActive;
                update.SubTitle = sliders.SubTitle;
                update.Title = sliders.Title;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sliders);
        }

        // GET: ManagementPanel/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // POST: ManagementPanel/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sliders sliders = db.Sliders.Find(id);
            db.Sliders.Remove(sliders);
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
