using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherReviews.Models;

namespace TeacherReviews.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }
       

        public ActionResult About()
        {
            ViewBag.Message = "A quick demo of an MVC5 web app deployed to Azure web sites.";

            return View();
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