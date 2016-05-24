using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRAM1.Models;
using Microsoft.AspNet.Identity;

namespace KRAM1.Controllers
{
    public class ImageController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        // GET: Image/Details/5
        public ActionResult Details(int id)
        {

            var list = context.Pictures.ToList();


            return View(list);
        }

        public ActionResult Submit()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var url = Request["imgurl"];
            var tags = Request["tags"];
            var userId = User.Identity.GetUserId();
            var user = context.Users.Where(u => u.Id == userId);

            if (url == null && tags == null)
            {

            }
            else
            {
                //   ApplicationUser currentUser = new ApplicationUser() { Id = userId };
                Hashtag hastags = new Hashtag() { Name = tags };
                Picture newImage = new Picture()
                {
                    PicUrl = url,
                    TimeStamp = DateTime.Now,
                    UserId = userId,
                    Hashtag = hastags
                };

                context.Pictures.Add(newImage);
                context.SaveChanges();
            }
            return View();
        }
        public ActionResult FullImage(int fileName)
        {


            ViewBag.showpic = context.Pictures.Find(fileName);

            return View(ViewBag.showpic);
        }
    }
}