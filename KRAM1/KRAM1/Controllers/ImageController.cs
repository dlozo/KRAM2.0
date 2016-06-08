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

            return View();
        }
        [HttpPost]
        public ActionResult SubmitImgURL(FormCollection collection)
        {
            try
            {

                string imageURL = collection["SubmitImgURL"];
                string gettag = collection["tags"];


                var userId = User.Identity.GetUserId();
                var tag = context.Hashtags.FirstOrDefault(x => x.Name == gettag);
                if (imageURL == string.Empty)
                {
                    ModelState.AddModelError(string.Empty, "Sorry, something went wrong, please try again.");

                }
                else
                {

                    if (tag == null)
                    {
                        Hashtag hastags = new Hashtag() { Name = gettag };
                        Picture newImage = new Picture()
                        {
                            PicUrl = imageURL,
                            TimeStamp = DateTime.Now,
                            UserId = userId,
                            Hashtag = hastags
                        };
                        context.Pictures.Add(newImage);
                        context.SaveChanges();
                        return Redirect("/Image/FullImage?fileName=" + newImage.Id);
                    }
                    else
                    {
                        Picture newImage = new Picture()
                        {
                            PicUrl = imageURL,
                            TimeStamp = DateTime.Now,
                            UserId = userId,
                            Hashtag = tag
                        };
                        context.Pictures.Add(newImage);
                        context.SaveChanges();
                        return Redirect("/Image/FullImage?fileName=" + newImage.Id);
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("Submit", "Sorry, something went wrong, please try again");
            }
            return Redirect("/Image/Submit");

        }
        //LikeOrDislike Man klickar på like eller dislike i View Modellen som har parametrarna PictureId eller bool trueor false
        public ActionResult LikedorDislikedImage(int pictureId, bool trueOrFalse)
        {
            ApplicationUser user = context.Users.Find(User.Identity.GetUserId());
            //Hittar rätt bild
            var picture = context.Pictures.Find(pictureId);

            //Hitta rätt user som likat
            var userId = context.Users.Find(User.Identity.GetUserId());

            var test = user.Reaction.FirstOrDefault(x => x.PictureId == pictureId);

            string message = $"{user.Name} has #like# your picture";

            if (test == null)
            {
                if (trueOrFalse)
                {
                    message = message.Replace("#like#", "liked");

                    context.Reactions.Add(new Reaction { LikeOrDislike = Reaction.ReactionType.Like, Picture = picture, User = userId });
                    context.Notifications.Add(new Notification { Message = message, UserId = user.Id, PictureId = pictureId, Time = DateTime.Now  });
                }
                else
                {
                    message = message.Replace("#like#", "disliked");
                    context.Reactions.Add(new Reaction { LikeOrDislike = Reaction.ReactionType.Dislike, Picture = picture, User = userId });
                    context.Notifications.Add(new Notification { Message = message, UserId = user.Id, PictureId = pictureId, Time = DateTime.Now });
                }
                context.SaveChanges();
            }
            else
            {
                if (trueOrFalse)
                {
                    if (test.LikeOrDislike == Reaction.ReactionType.Like)
                    {
                        context.Reactions.Remove(test);
                        var userMe = User.Identity.GetUserId();
                        var removeNot = context.Notifications.FirstOrDefault(x => x.User.Id == userMe && x.PictureId == pictureId);
                        if (removeNot != null)
                        {
                            context.Notifications.Remove(removeNot);

                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        test.LikeOrDislike = Reaction.ReactionType.Like;
                        context.SaveChanges();
                    }
                }

                else
                {
                    if (test.LikeOrDislike == Reaction.ReactionType.Dislike)
                    {
                        context.Reactions.Remove(test);
                        var userMe = User.Identity.GetUserId();
                        var removeNot = context.Notifications.FirstOrDefault(x => x.User.Id == userMe && x.PictureId == pictureId);
                        if (removeNot != null)
                        {
                            context.Notifications.Remove(removeNot);

                        }
                        context.SaveChanges();

                    }
                    else
                    {
                        test.LikeOrDislike = Reaction.ReactionType.Dislike;
                        context.SaveChanges();
                    }

                }
            }


            return Redirect("/Image/FullImage?fileName=" + pictureId);
        }
        public ActionResult FullImage(int fileName)
        {
            var x = context.Pictures.Find(fileName);
            var t = x.Id;
            var p = context.Reactions.Where(a => a.Picture == x);

            var getCurrentUser = User.Identity.GetUserId();
            var currentUser = context.Users.FirstOrDefault(o => o.Id == getCurrentUser);

            ViewBag.IsAdmin = false;

            if (getCurrentUser == "7cc96f03-8e56-40a3-8a46-3928dce1aef6")
            {
                ViewBag.IsAdmin = true;
            }

            // hämtar ALLA dislikes
            var amountOfDislike = x.Reaction.Count(y => y.LikeOrDislike == Reaction.ReactionType.Dislike);
            // hämtar ALLA likes
            var amountOfLikes = x.Reaction.Count(y => y.LikeOrDislike == Reaction.ReactionType.Like);

            //Skriver ut LIKES
            ViewBag.addLike = amountOfLikes;
            //Skriver ut DISLIKES
            ViewBag.addDislike = amountOfDislike;

            ViewBag.Hashtag = x.Hashtag;
            ViewBag.x = x.PicUrl;

            ViewBag.Id = x.Id;
            ViewBag.Comments = context.Comments.Where(l => l.PictureId == t);
            if (x.Hashtag != null)
            {

                var zx = context.Users.Find(x.UserId);
                ViewBag.User = zx;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            Picture newPicture = new Picture();

            ApplicationDbContext context = new ApplicationDbContext();

            if (file == null)
            {
                ModelState.AddModelError("File", "Please Upload Your imgfile");
            }
            else
            {
                FileSave(file);
                var userId = User.Identity.GetUserId();

                var user = context.Users.Find(userId);
                var x = user.UploadedPicId;
                var c = from a in context.Pictures
                        where a.UserId == userId
                        orderby a.Id descending
                        select a.Id;

                int p = c.First();
                return RedirectToAction("FullImage", "Image", new { fileName = p });
            }
            return RedirectToAction("Submit");
        }
        public ActionResult HashImg(int fileName)
        {
            var x = context.Pictures.Find(fileName);
            ViewBag.hash = x.Hashtag.Picture;

            return View();
        }

        public JsonResult HashtagSearch(string hashtag)
        {
            var hashtaglist = context.Hashtags;

            //http://stackoverflow.com/questions/26206288/entity-to-json-error-a-circular-reference-was-detected-while-serializing-an-ob
            //Varför ProxyCreationEnabled behövs ^
            context.Configuration.ProxyCreationEnabled = false;
            var hashtagResults = (from p in hashtaglist
                                  where p.Name.ToLower().Contains(hashtag.ToLower())
                     select new { p.Name }).Distinct();
            //foreach (var tag in context.Hashtags.ToList())
            //{
            //    if (tag.Name.ToLower().Contains(hashtag.ToLower()))
            //    {
            //        hashtagResults.Add(tag);
            //    }
            //}
            if (hashtagResults == null)
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
        public ActionResult UpdateProfile(HttpPostedFileBase file)
        {
            var url = Request["imgurl"];
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);
            try
            {  //Om man har url bara så går den in här
                if (file == null && url != null)
                {
                    user.ProfilePic = url;
                    context.SaveChanges();
                }
                else //annars kör den fileSave metoden som sparar filen man laddar upp.
                {
                    FileSave(file);
                }
                return RedirectToAction("Index" + "/" + userId, "User");
            }
            catch
            {
                ViewBag.Message = "You done fucked up";
                return RedirectToAction("Index" + "/" + userId, "User");
            }
        }
        public void FileSave(HttpPostedFileBase file)
        {
            //var photo = System.Web.Helpers.WebImage.GetImageFromRequest();
            var url = Request["imgurl"];
            var gettag = Request["tags"];
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);
            var tag = context.Hashtags.FirstOrDefault(x => x.Name == gettag);

            try
            {


                if (file == null && url == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your imgfile");
                }
                if (file != null)
                {
                    var z = Request.Path;

                    string extension = Path.GetExtension(file.FileName);
                    var fileNames = Path.GetFileName(file.FileName);
                    var guid = Guid.NewGuid().ToString(); //Randomizer filnamnet

                    if (z.Contains("Update"))
                    {

                        file.ValidateImageFile();
                        var path = Path.Combine(Server.MapPath("~/uploads/profile"), guid + fileNames); //Får fram fullständiga mappen man sparar i. Vi får ändra till server mappen senare.
                        string fl = path.Substring(path.LastIndexOf("\\"));
                        string[] split = fl.Split('\\');
                        string newpath = split[1];
                        string imagepath = "~/uploads/profile/" + newpath;

                        if (user.ProfilePic != null && !user.ProfilePic.Contains("nophoto.png")) //Tar bort bilden från mappen när du byter profil bild
                        {
                            System.IO.File.Delete(Server.MapPath("~/uploads/profile/") + Path.GetFileName(user.ProfilePic));
                        }
                        //Binder till bildtabellen i databasen
                        user.ProfilePic = imagepath; //Får nog kanske göra om imagepath / path senare när vi laddar upp den till en server.
                        file.SaveAs(path); //Sparar till en mapp ~/uploads/
                        context.SaveChanges();
                        ModelState.Clear();
                        ViewBag.Message = "Image uploaded successfully";
                    }
                    else
                    {
                        Picture newPicture = new Picture();
                        file.ValidateImageFile();
                        var path = Path.Combine(Server.MapPath("~/uploads"), guid + fileNames); //Får fram fullständiga mappen man sparar i. Vi får ändra till server mappen senare.

                        string fl = path.Substring(path.LastIndexOf("\\"));
                        string[] split = fl.Split('\\');
                        string newpath = split[1];

                        string imagepath = "~/uploads/" + newpath;
                        if (tag == null)
                        {
                            Hashtag hastags = new Hashtag() { Name = gettag };

                            //Binder till bildtabellen i databasen
                            newPicture.PicUrl = imagepath; //Får nog kanske göra om imagepath / path senare när vi laddar upp den till en server.
                            newPicture.TimeStamp = DateTime.Now;
                            newPicture.Hashtag = hastags;
                            newPicture.UserId = userId;
                        }
                        else
                        {
                            //Binder till bildtabellen i databasen
                            newPicture.PicUrl = imagepath; //Får nog kanske göra om imagepath / path senare när vi laddar upp den till en server.
                            newPicture.TimeStamp = DateTime.Now;
                            newPicture.Hashtag = tag;
                            newPicture.UserId = userId;
                        }

                        file.SaveAs(path); //Sparar till en mapp ~/uploads/
                        context.Pictures.Add(newPicture);
                        context.SaveChanges();
                        ModelState.Clear();
                        ViewBag.Message = "Image uploaded successfully";
                    }
                }
            }
            catch (Exception)
            {


            }
        }


        public ActionResult Notification()
        {
            ApplicationDbContext coontext = new ApplicationDbContext();
            var getUser = User.Identity.GetUserId();
            var list = coontext.Notifications.Where(x => x.Picture.User.Id == getUser);

            var hasRead = coontext.Notifications.Where(x => x.HasRead == false);

            foreach (var item in hasRead)
            {
                item.HasRead = true;
            }

            coontext.SaveChanges();
            return View(list);
        }
        public ActionResult DeleteNotification(int notificationId)
        {
            var currentNotification = context.Notifications.Find(notificationId);
            context.Notifications.Remove(currentNotification);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult NotificationCount()
        {
            ApplicationDbContext coontext = new ApplicationDbContext();
            var getUser = User.Identity.GetUserId();
            var count = coontext.Notifications.Where(x => x.Picture.User.Id == getUser && x.HasRead == false).Count();

            return Json(count, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int ID)
        {
            try
            {
                var x = context.Pictures.FirstOrDefault(i => i.Id == ID);
                var o = Request.Url.AbsoluteUri;
                if (x != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/uploads/") + Path.GetFileName(x.PicUrl));
                    context.Pictures.Remove(x);
                }
                
                //context.Entry(x).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                ViewBag.Message = "Your photo was sucessfully deleted";

            }
            catch (Exception)
            {

                ModelState.AddModelError("FileSave", "Sorry, something went wrong, please try again");
            }


            return RedirectToAction("Index" + "/" + User.Identity.GetUserId(), "User");
        }

    }

}

