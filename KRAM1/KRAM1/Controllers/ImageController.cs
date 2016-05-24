using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRAM1.Models;
namespace KRAM1.Controllers
{
    public class ImageController : Controller
    { ApplicationDbContext context = new ApplicationDbContext();
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

        public ActionResult Submit(string Id)
        {

           
            var url = Request["imgurl"];
            var tags = Request["tags"];

            if(url==null&&tags==null)
                {

                }
            else {
                Hashtag hastags = new Hashtag() { Name=tags };
              

              
                  Picture newimage = new Picture() { PicUrl = url, TimeStamp=DateTime.Now, User = context.Users.Find(Id) };

                context.Hashtags.Add(hastags);
                context.Pictures.Add(newimage);
                context.SaveChanges();
            }

            return View();
        }
        public ActionResult FullImage(int fileName)
        {
            

            ViewBag.showpic  = context.Pictures.Find(fileName);

            return View(ViewBag.showpic);
        }
    }
}