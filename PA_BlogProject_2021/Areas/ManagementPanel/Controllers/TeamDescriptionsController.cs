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
    public class TeamDescriptionsController : Controller
    {
        private BlogProjeContext db = new BlogProjeContext();

        // GET: ManagementPanel/TeamDescriptions
        public ActionResult Index()
        {
            return View(db.TeamDescriptions.ToList());
        }

        // GET: ManagementPanel/TeamDescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDescriptions teamDescriptions = db.TeamDescriptions.Find(id);
            if (teamDescriptions == null)
            {
                return HttpNotFound();
            }
            return View(teamDescriptions);
        }

        // GET: ManagementPanel/TeamDescriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/TeamDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamDescriptionId,FirstTitle,SubTitle,Title,Description,ImageURL,IsActive")] TeamDescriptions teamDescriptions,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile!=null)
                {
                    teamDescriptions.ImageURL = ImageUpload.SaveImage(imgFile, 1600, 1067);
                }
                db.TeamDescriptions.Add(teamDescriptions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teamDescriptions);
        }

        // GET: ManagementPanel/TeamDescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDescriptions teamDescriptions = db.TeamDescriptions.Find(id);
            if (teamDescriptions == null)
            {
                return HttpNotFound();
            }
            return View(teamDescriptions);
        }

        // POST: ManagementPanel/TeamDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamDescriptionId,FirstTitle,SubTitle,Title,Description,ImageURL,IsActive")] TeamDescriptions teamDescriptions,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                TeamDescriptions update = db.TeamDescriptions.Find(teamDescriptions.TeamDescriptionId);

                if (imgFile!=null)
                {
                    ImageUpload.DeleteByPath(update.ImageURL);
                    update.ImageURL = ImageUpload.SaveImage(imgFile, 1600, 1067);
                }

                update.Description = teamDescriptions.Description;
                update.FirstTitle = teamDescriptions.FirstTitle;
                update.IsActive = teamDescriptions.IsActive;
                update.SubTitle = teamDescriptions.SubTitle;
                update.Title = teamDescriptions.Title;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamDescriptions);
        }

        // GET: ManagementPanel/TeamDescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDescriptions teamDescriptions = db.TeamDescriptions.Find(id);
            if (teamDescriptions == null)
            {
                return HttpNotFound();
            }
            return View(teamDescriptions);
        }

        // POST: ManagementPanel/TeamDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamDescriptions teamDescriptions = db.TeamDescriptions.Find(id);
            db.TeamDescriptions.Remove(teamDescriptions);
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
