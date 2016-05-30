using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRAM1.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.Routing;
using System.Drawing;
using System.Web.Script.Serialization;

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
            var gettag = Request["tags"];
            var userId = User.Identity.GetUserId();
            var tag = context.Hashtags.FirstOrDefault(x => x.Name == gettag);
            if (url == null && gettag == null)
            {

            }
            else
            {
                //Hashtag hastags = new Hashtag() { Name = gettag };
                //Picture newImage = new Picture()
                //{
                //    PicUrl = url,
                //    TimeStamp = DateTime.Now,
                //    UserId = userId,
                //    Hashtag = hastags
                //};
                //context.Pictures.Add(newImage);
                //context.SaveChanges();
                if (tag == null)
                {
                    Hashtag hastags = new Hashtag() { Name = gettag };
                    Picture newImage = new Picture()
                    {
                        PicUrl = url,
                        //TimeStamp = DateTime.Now,
                        UserId = userId,
                        Hashtag = hastags
                    };
                    context.Pictures.Add(newImage);
                    context.SaveChanges();
                }
                else
                {
                    Picture newImage = new Picture()
                    {
                        PicUrl = url,
                        // TimeStamp = DateTime.Now,
                        UserId = userId,
                        Hashtag = tag
                    };
                    context.Pictures.Add(newImage);
                    context.SaveChanges();
                }
            }
            return View();
        }
        public ActionResult FullImage(int fileName)
        {
            var x = context.Pictures.Find(fileName);

            ViewBag.Hashtag = x.Hashtag;
            ViewBag.x = x.PicUrl;
            return View();
        }
        [HttpPost]

        public ActionResult Upload(Picture newPicture, HttpPostedFileBase file)
        {
            int fileName = 0;
            ApplicationDbContext context = new ApplicationDbContext();

            if (file == null)
            {
                ModelState.AddModelError("File", "Please Upload Your imgfile");
            }
            else
            {
                file.ValidateImageFile();
                string extension = Path.GetExtension(file.FileName);
                var tags = Request["tags"];

                var userId = User.Identity.GetUserId();


                var fileNames = Path.GetFileName(file.FileName);
                var guid = Guid.NewGuid().ToString(); //Randomizer filnamnet
                var path = Path.Combine(Server.MapPath("~/uploads"), guid + fileNames); //Får fram fullständiga mappen man sparar i. Vi får ändra till server mappen senare.

                string fl = path.Substring(path.LastIndexOf("\\"));
                string[] split = fl.Split('\\');

                string newpath = split[1];

                string imagepath = "~/uploads/" + newpath;
                Hashtag hastags = new Hashtag() { Name = tags };
                //Binder till bildtabellen i databasen
                newPicture.PicUrl = imagepath; //Får nog kanske göra om imagepath / path senare när vi laddar upp den till en server.
                                               // newPicture.TimeStamp = DateTime.Now;
                newPicture.Hashtag = hastags;
                newPicture.UserId = userId;


                file.SaveAs(path); //Sparar till en mapp ~/uploads/
                context.Pictures.Add(newPicture);
                context.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Image uploaded successfully";
                fileName = newPicture.Id;
                return RedirectToAction("FullImage", "Image", new { fileName = fileName });
            }

            //TempData["Success"]="Upload successful";
            return RedirectToAction("Index");
        }
        public ActionResult HashImg(int fileName)
        {
            var x = context.Pictures.Find(fileName);
            ViewBag.hash = x.Hashtag.Picture;

            return View();
        }

        public ActionResult HashtagSearch(string hashtag)
        {
            var hashtagResults = new List<Hashtag>();

            //http://stackoverflow.com/questions/26206288/entity-to-json-error-a-circular-reference-was-detected-while-serializing-an-ob
            //Varför ProxyCreationEnabled behövs ^
            context.Configuration.ProxyCreationEnabled = false;

            foreach (var tag in context.Hashtags.ToList())
            {
                if (tag.Name.ToLower().Contains(hashtag.ToLower()))
                {
                    hashtagResults.Add(tag);
                }
            }
            if (hashtagResults.Count == 0)
            {
                return Json("No hashtag founds", JsonRequestBehavior.AllowGet);
            }

            return Json(hashtagResults, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImageSearch(string searchInput)
        {
            if (String.IsNullOrEmpty(searchInput))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var pictureResults = new List<Picture>();

                foreach (var picture in context.Pictures.ToList())
                {
                    if (picture.Hashtag.Name.ToLower().Contains(searchInput.ToLower()))
                    {
                        pictureResults.Add(picture);
                    }
                }
                return View(pictureResults);
            }
        }
    }
}


