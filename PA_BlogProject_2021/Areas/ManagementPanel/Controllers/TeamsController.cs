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
    public class TeamsController : Controller
    {
        private BlogProjeContext db = new BlogProjeContext();

        // GET: ManagementPanel/Teams
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: ManagementPanel/Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            if (teams == null)
            {
                return HttpNotFound();
            }
            return View(teams);
        }

        // GET: ManagementPanel/Teams/Create
        public ActionResult Create()
        {
       
            return View();
        }

        // POST: ManagementPanel/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,PersonImage,PersonName,PersonJob,TwitterURL,FacebookURL,LinkedinURL,InstagramURL,IsActive,RegisterDate")] Teams teams, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {

                if (imgFile!=null)
                {
                    teams.PersonImage = ImageUpload.SaveImage(imgFile, 400, 550);
                }
                teams.RegisterDate = DateTime.Now;

                db.Teams.Add(teams);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teams);
        }

        // GET: ManagementPanel/Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            if (teams == null)
            {
                return HttpNotFound();
            }
            return View(teams);
        }

        // POST: ManagementPanel/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,PersonImage,PersonName,PersonJob,TwitterURL,FacebookURL,LinkedinURL,InstagramURL,IsActive,RegisterDate")] Teams teams,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                Teams upload = db.Teams.Find(teams.TeamId);

                if (imgFile!=null)
                {

                    ImageUpload.DeleteByPath(upload.PersonImage);
                    upload.PersonImage = ImageUpload.SaveImage(imgFile, 400, 550);
                }
                upload.InstagramURL = teams.InstagramURL;
                upload.FacebookURL = teams.FacebookURL;
                upload.LinkedinURL = teams.LinkedinURL;
                upload.PersonJob = teams.PersonJob;
                upload.PersonName = teams.PersonName;
                upload.TwitterURL = teams.TwitterURL;
                upload.RegisterDate = DateTime.Now;
          
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teams);
        }

        // GET: ManagementPanel/Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            if (teams == null)
            {
                return HttpNotFound();
            }
            return View(teams);
        }

        // POST: ManagementPanel/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teams teams = db.Teams.Find(id);

            ImageUpload.DeleteByPath(teams.PersonImage);

            db.Teams.Remove(teams);
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
