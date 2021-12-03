using PA_BlogProject_2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_BlogProject_2021.Areas.ManagementPanel.Controllers
{
    public class AccountController : Controller
    {
        // GET: ManagementPanel/Account
        BlogProjeContext db = new BlogProjeContext();
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name, string Email, string Password)
        {
            Users user = new Users();
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                user.UserName = Name;
                user.Email = Email;
                user.Password = Password;
                user.IsActive = true;
                user.RegisterDate = DateTime.Now;

                foreach (var item in db.Roles)
                {

                    if (item.RolName == "User")
                    {
                        user.RolId = item.RolId;

                    }

                }
                db.Users.Add(user);
                db.SaveChanges();

            }
            else
            {
                return View("Veriler boş geldi");
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {

            Users user = db.Users.FirstOrDefault(u => u.Password == Password && u.Email == Email);

            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (user.RolId == 1)
            {
                Session["KullanıcıAdı"] = user.UserName;
                return RedirectToAction("Index", "Blogs", new { area = "ManagementPanel" });
            }
            else
            {
                Session["KullanıcıAdı"] = user.UserName;
                return RedirectToAction("Index", "Home");
            }


        }

        public ActionResult Logout()
        {

            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}