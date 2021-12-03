using PA_BlogProject_2021.Models;
using PA_BlogProject_2021.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_BlogProject_2021.Controllers
{
    public class HomeController : Controller
    {
        BlogProjeContext db = new BlogProjeContext();
        public ActionResult Index()
        {   ///sliders
            //blogs

            var model = new IndexViewModel();
            model.Services = db.Services.Where(s => s.IsActive == true).ToList();
            model.Blogs = db.Blogs.Where(b => b.IsActive == true).ToList();
            model.Sliders = db.Sliders.Where(s => s.IsActive == true).ToList();
            model.TeamDescriptions = db.TeamDescriptions.Where(u=>u.IsActive==true).ToList();
            model.Contacts = db.Contacts.Where(u=>u.IsActive==true).ToList();

            return View(model);
        }

        //public PartialViewResult Don()/// burası!!
        //{
        //    var model = db.Sliders.ToList();

        //    return PartialView("_IndexSlider", model);
        //}


        public ActionResult About()
        {
            var model = new AboutViewModel();

            model.Abouts = db.Abouts.Where(u => u.IsActive == true).ToList();
            model.Teams = db.Teams.Where(t => t.IsActive == true).ToList();
            //hakkımda
            //teams
            return View(model);
        }

        public ActionResult Services()
        {
            var model = db.Services.Where(s => s.IsActive == true).ToList();
            return View(model);
        }

        public ActionResult Portfolio()
        {
            var model = db.Portfolios.Where(p => p.IsActive == true).ToList();
            return View(model);
        }
        public ActionResult Blog()
        {
            var model = new BlogViewModel();
            model.Blogs = db.Blogs.Where(b => b.IsActive == true).ToList();
            model.Tags = db.Tags.Where(t => t.IsActive == true).ToList();

            return View(model);
        
        }

        public ActionResult BlogComments(int BlogId,string name,string country,string comment,int?IsReply)
        {
            Comments comments = new Comments();
            comments.BlogId = BlogId;

            if (IsReply!=null)
            {
                comments.IsReply = IsReply.Value;
            }

            comments.CommentatorName = name;
            comments.CommentatorCountry = country;
            comments.Comment = comment;
            comments.RegisterDate = DateTime.Now;
            db.Comments.Add(comments);
            db.SaveChanges();

            var model = db.Comments.Where(u => u.BlogId == BlogId).ToList();

            return PartialView("_CommentList",model);
        }


        public ActionResult TagFilter(int TagId)
        {
            var model = db.Blogs.Where(u => u.IsActive == true).ToList();
            if (TagId!=0&& model.Any(b=>b.Tags.Any(t=>t.TagId==TagId)))
            {
                model = db.Blogs.Where(b => b.Tags.Any(t => t.TagId == TagId)).ToList();
            }
            else
            {
                model = db.Blogs.Where(u => u.IsActive == true).ToList();
            }

            return PartialView("_BlogFilter",model);
        }
  

        public ActionResult BlogDetails(int id)
        {
            var model = db.Blogs.Where(d => d.BlogId == id).ToList();
            return View(model);
        }

        public ActionResult Contact()
        {
            var model = db.Contacts.Where(c => c.IsActive == true).ToList();
            return View(model);
        }
    }
}