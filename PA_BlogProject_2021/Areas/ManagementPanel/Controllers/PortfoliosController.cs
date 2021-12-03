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
    public class PortfoliosController : Controller
    {
        private BlogProjeContext db = new BlogProjeContext();

        // GET: ManagementPanel/Portfolios
        public ActionResult Index()
        {
            return View(db.Portfolios.ToList());
        }

        // GET: ManagementPanel/Portfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return HttpNotFound();
            }
            return View(portfolios);
        }

        // GET: ManagementPanel/Portfolios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Portfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PortfoliosId,ImageURL,ProjeName,ProjeCategory,IsActive,RegisterDate")] Portfolios portfolios,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {

                if (imgFile!=null)
                {
                    portfolios.ImageURL = ImageUpload.SaveImage(imgFile,600,446);
                }


                portfolios.RegisterDate = DateTime.Now;
                db.Portfolios.Add(portfolios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portfolios);
        }

        // GET: ManagementPanel/Portfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return HttpNotFound();
            }
            return View(portfolios);
        }

        // POST: ManagementPanel/Portfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PortfoliosId,ImageURL,ProjeName,ProjeCategory,IsActive,RegisterDate")] Portfolios portfolios,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                Portfolios update = db.Portfolios.Find(portfolios.PortfoliosId);

                if (imgFile!=null)
                {
                    ImageUpload.DeleteByPath(update.ImageURL);
                    update.ImageURL = ImageUpload.SaveImage(imgFile, 600, 446);
                }

                update.IsActive = portfolios.IsActive;
                update.ProjeCategory = portfolios.ProjeCategory;
                update.ProjeName = portfolios.ProjeName;
                update.RegisterDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portfolios);
        }

        // GET: ManagementPanel/Portfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return HttpNotFound();
            }
            return View(portfolios);
        }

        // POST: ManagementPanel/Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Portfolios portfolios = db.Portfolios.Find(id);
            ImageUpload.DeleteByPath(portfolios.ImageURL);
            db.Portfolios.Remove(portfolios);
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
