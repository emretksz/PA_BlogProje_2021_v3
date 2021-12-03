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
    public class UsersController : Controller
    {
        private BlogProjeContext db = new BlogProjeContext();

        // GET: ManagementPanel/Users
        public ActionResult Index()
        {
           
            //var users = db.Users.Include(u => u.Roles);
            return View(db.Users.ToList());
        }

        // GET: ManagementPanel/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: ManagementPanel/Users/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName");
            //ViewBag.Roller = db.Roles.ToList();
 
            // Viewbag'e RolId adında bir isim vermiş. 
            //RolId'im birden çok rol içereceği için bir liste dönmek zorunda.
            // SelectList metodu benim liste dönmemi sağlayan  be sistem collections'dan gelen bir metoddur.
            // benden bir liste ister, o listenin Id'si ve o ıd'nin text olarak gösterileceği string ifadeyi ister.
            // biz roller tablosunu verdik. Bu tablo rolId tuttuyor ve göstereceği isim ise RolName'de tutulan rol ismi olacaktır.
            return View();
        }

        // POST: ManagementPanel/Users/Create
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Email,Password,IsActive,RegisterDate,RolId")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.RegisterDate = DateTime.Now;

                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName", users.RolId);
            return View(users);
        }

        // GET: ManagementPanel/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName", users.RolId);
            return View(users);
        }

        // POST: ManagementPanel/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Email,Password,IsActive,RegisterDate,RolId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName", users.RolId);
            return View(users);
        }

        // GET: ManagementPanel/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: ManagementPanel/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
