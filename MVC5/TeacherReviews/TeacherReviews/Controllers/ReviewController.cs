using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeacherReviews.Models;

namespace TeacherReviews.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index(int id)
        {
            var teacher = db.Teachers.Find(id);

            if (teacher != null)
            {
                return View(teacher);
            }
            return HttpNotFound();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int id,string review)
        {
            if (HttpContext.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(review))
            {
                var teacher = db.Teachers.Find(id);

                if (teacher != null)
                {
                    if (!db.TeacherReviews.Where(x => x.TeacherId == id && x.ReviewerName == HttpContext.User.Identity.Name).Any())
                    {
                        db.TeacherReviews.Add(new TeacherReview() { Review = review, ReviewerName = HttpContext.User.Identity.Name, TeacherId = id });
                        db.SaveChanges();
                    }
                    //return RedirectToAction("Index", "Review", new { id = id });
                    return RedirectToRoute("reviews", new { id = id });    
                   
                }
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Review", new { id = id});       
        }


        [Authorize]
        public ActionResult Clear(int id)
        {
            var teacher = db.Teachers.Find(id);

            if (teacher != null)
            {
                db.TeacherReviews.RemoveRange(db.TeacherReviews);
                db.SaveChanges();

                return RedirectToRoute("reviews", new { id = id });
            }
            return HttpNotFound();
        }

        [Authorize]
        public ActionResult Delete(int id,int pid)
        {
            var teacherReview = db.TeacherReviews.Find(id);

            if (teacherReview != null)
            {
                db.TeacherReviews.Remove(teacherReview);
                db.SaveChanges();

                return RedirectToRoute("reviews", new { id = pid });
            }
            return HttpNotFound();
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
