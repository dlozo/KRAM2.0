using KRAM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRAM1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var list = context.Pictures.ToList();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FullImage(int fileName)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            ViewBag.showpic = context.Pictures.Find(fileName);

            return View(ViewBag.showpic);
        }
    }
}