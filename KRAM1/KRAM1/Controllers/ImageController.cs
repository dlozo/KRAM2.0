using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRAM1.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace KRAM1.Controllers
    {
    public class ImageController : Controller
        {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Image
        public ActionResult Index()
            {
            var model = context.Pictures.ToList();
            return View(model);
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
            var user = context.Users.Where(u => u.Id==userId);

            if(url==null&&tags==null)
                {

                }
            else
                {
                //   ApplicationUser currentUser = new ApplicationUser() { Id = userId };
                Hashtag hastags = new Hashtag() { Name=tags };
                Picture newImage = new Picture()
                    {
                    PicUrl=url,
                    TimeStamp=DateTime.Now,
                    UserId=userId,
                    Hashtag=hastags
                    };

                context.Pictures.Add(newImage);
                context.SaveChanges();
                }
            return View();
            }
        public ActionResult FullImage(int fileName)
            {

           var x = context.Pictures.Find(fileName);

            ViewBag.x=x.PicUrl;
            return View();
            }
        [HttpPost]

        public ActionResult Upload(Picture newPicture, HttpPostedFileBase file)
            {
            ApplicationDbContext context = new ApplicationDbContext();


if(file.ContentLength > 0 && file.ContentType.Contains("image"))
                {
                var tags = Request["tags"];
                var url = Request["id"];
                var userId = User.Identity.GetUserId();
                var user = context.Users.Where(u => u.Id==userId);

                var fileName = Path.GetFileName(file.FileName);
                var guid = Guid.NewGuid().ToString(); //Randomizer filnamnet
                var path = Path.Combine(Server.MapPath("~/uploads"), guid+fileName); //Får fram fullständiga mappen man sparar i. Vi får ändra till server mappen senare.

                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');

                string newpath = split[1];
                string imagepath = "~/uploads/"+newpath;
                Hashtag hastags = new Hashtag() { Name=tags };
                newPicture.PicUrl=imagepath; //Får nog kanske göra om imagepath / path senare när vi laddar upp den till en server.
                newPicture.TimeStamp=DateTime.Now;
                newPicture.Hashtag=hastags;
                newPicture.UserId=userId;

                file.SaveAs(path); //Sparar till en mapp

               
             
                context.Pictures.Add(newPicture);
                context.SaveChanges();

                }
            else
                {
                ViewBag.Error="You either ";
                }
            TempData["Success"]="Upload successful";
            return RedirectToAction("Index");
            }
        }
        
    }