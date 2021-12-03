using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PA_BlogProject_2021.Models;

namespace PA_BlogProject_2021.Areas.ManagementPanel.Controllers
{
    public class AboutsController : Controller
    {
        private BlogProjeContext db = new BlogProjeContext();

        // GET: ManagementPanel/Abouts
        public ActionResult Index()
        {
            return View(db.Abouts.ToList());
        }

        // GET: ManagementPanel/Abouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abouts abouts = db.Abouts.Find(id);
            if (abouts == null)
            {
                return HttpNotFound();
            }
            return View(abouts);
        }

        // GET: ManagementPanel/Abouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Abouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "AboutId,Title,SubTitle,Description,MissionTitle,MissonDescription,VissionTitle,VissionDescription,ApproachTitle,ApproachDescriptin,IsActive,RegisterDate")] Abouts abouts)
        {
            if (ModelState.IsValid)
            {
                db.Abouts.Add(abouts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(abouts);
        }

        // GET: ManagementPanel/Abouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abouts abouts = db.Abouts.Find(id);
            if (abouts == null)
            {
                return HttpNotFound();
            }
            return View(abouts);
        }

        // POST: ManagementPanel/Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "AboutId,Title,SubTitle,Description,MissionTitle,MissonDescription,VissionTitle,VissionDescription,ApproachTitle,ApproachDescriptin,IsActive,RegisterDate")] Abouts abouts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abouts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(abouts);
        }

        // GET: ManagementPanel/Abouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abouts abouts = db.Abouts.Find(id);
            if (abouts == null)
            {
                return HttpNotFound();
            }
            return View(abouts);
        }

        // POST: ManagementPanel/Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Abouts abouts = db.Abouts.Find(id);
            db.Abouts.Remove(abouts);
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
