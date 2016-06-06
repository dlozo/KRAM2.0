using KRAM1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
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
            if (Request["buttonAdd"] != null)
            {



                string allaFel = "";
                string nameVariabel = Request["inputText"];
                string nameEmail = Request["inputEmail"];
                
                string nameTextArea = Request["inputTextArea"];

                if (nameEmail == "")
                {
                    allaFel += "Du glömde skriva in Email!";
                }
                else
                {
                    bool ok = Regex.IsMatch(nameEmail,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    if (!ok)
                    {
                        allaFel += "Skriv in rätt format på Email IDIOT!!!! skriva in Email!";
                    }
                }

                if (nameVariabel == "")
                {
                    allaFel += "Du glömde skriva in ditt namn!";
                }
                
                if (nameTextArea == "")
                {
                    allaFel += "Skriv in ett meddelande, så vi vet vad du behöver hjälp med";
                }
                if (allaFel != "")
                {
                    ViewBag.Felmeddelande = allaFel;

                    ViewBag.nameVariabel = Request["inputText"];
                    ViewBag.nameEmail = Request["inputEmail"];
                    
                    ViewBag.nameTextArea = Request["inputTextArea"];
                }

                if (allaFel == "")
                {
                    return Redirect("FormulärSkickat");
                }

            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FullImage(int fileId)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            ViewBag.showpic = context.Pictures.Find(fileId);

            return View(ViewBag.showpic);
        }
        //public ActionResult AddComment()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    string comment = Request["comment"];
        //    int pictureId = Convert.ToInt16(Request["articleCommented"]);
        //    var userId = User.Identity.GetUserId();
        //    var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();

        //    Comment newComment = new Comment
        //    {
        //        TimeStamp = DateTime.Now,
        //        PictureId = pictureId,
        //        User = user,
        //        Text = comment

        //    };
        //    context.Comments.Add(newComment);
        //    context.SaveChanges();

        //    return Redirect("/Image/FullImage" + "?filename="+ pictureId);
        //}

        [System.Web.Http.HttpPost]
        public ActionResult PostComment(int pictureId, string comment)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string comment1 = comment;
            int pictureId1 = pictureId;
            var userId = User.Identity.GetUserId();
            var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();

            Comment newComment = new Comment
            {
                TimeStamp = DateTime.Now,
                PictureId = pictureId,
                UserId = user.Id,
                Text = comment,
                UserName = user.Name

            };
            context.Comments.Add(newComment);
            context.SaveChanges();
            return Json(newComment, JsonRequestBehavior.AllowGet);

        }

    }
}