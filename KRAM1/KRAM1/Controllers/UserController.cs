using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRAM1.Models;
using System.IO;
using System.Data.Entity;

namespace KRAM1.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationUser AppUser = new ApplicationUser();

        // GET: User
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var anonuserid = Request.Url.ToString();

                new Uri(anonuserid).Segments.Last();
                anonuserid.Substring(anonuserid.LastIndexOf("/") + 1);
                string s = anonuserid.Substring(anonuserid.LastIndexOf("/") + 1);


                if (!anonuserid.Contains("/User"))
                {
                    UserIdentity();
                }
                var request = Request.LogonUserIdentity;
                var userId = User.Identity.GetUserId();
                var user = context.Users.Find(userId);

                var userViewModel = new UserViewModel
                {
                    UserEmail = user.Email,
                    ProfilePic = user.ProfilePic,
                    UserName = user.Name,
                    UserID = user.Id,
                    Pictures = new List<Picture>(),
                    IsOwner = false,
                };

                if (user.UserName == User.Identity.Name)
                    userViewModel.IsOwner = true;
                var x = context.Pictures.Where(a => a.UserId == userId);

                if (x != null)
                {
                    foreach (var c in x)
                    {
                        userViewModel.Pictures.Add(c);
                        foreach (var item in c.Reaction)
                        {
                            userViewModel.TotalLikes++;
                        }

                    }


                    return View(userViewModel);
                }



                else
                {
                    UserIdentity();
                    //var anonuserid = Request.Url.ToString();
                    //new Uri(anonuserid).Segments.Last();
                    //anonuserid.Substring(anonuserid.LastIndexOf("/") + 1);
                    //string s = anonuserid.Substring(anonuserid.LastIndexOf("/") + 1);

                    //var anon = context.Users.Find(s);
                    //var userViewModel = new UserViewModel
                    //{
                    //    UserEmail = anon.Email,
                    //    ProfilePic = anon.ProfilePic,
                    //    UserName = anon.UserName,
                    //    UserID = anon.Id,
                    //    Pictures = new List<Picture>(),
                    //    IsOwner = false,
                    //};
                    //var x = context.Pictures.Where(a => a.UserId == anon.Id);

                    //if (x != null)
                    //{
                    //    foreach (var c in x)
                    //    {
                    //        userViewModel.Pictures.Add(c);
                    //        foreach (var item in c.Reaction)
                    //        {
                    //            userViewModel.TotalLikes++;
                    //        }

                    //    }
                    UserViewModel model = (UserViewModel)TempData["userViewModel"];
                    return View(model);






                }

            }
            return View();
        }
        [HttpPost]

        public ActionResult UserIdentity()
        {
            var anonuserid = Request.Url.ToString();

            new Uri(anonuserid).Segments.Last();
            anonuserid.Substring(anonuserid.LastIndexOf("/") + 1);
            string s = anonuserid.Substring(anonuserid.LastIndexOf("/") + 1);

            var userId2 = User.Identity.GetUserId();

            //if (User.Identity.IsAuthenticated)
            //{
            //    var userId = User.Identity.GetUserId();
            //    var user = context.Users.Find(userId);



            //    var userViewModel = new UserViewModel
            //    {
            //        UserEmail = user.Email,
            //        ProfilePic = user.ProfilePic,
            //        UserName = user.UserName,
            //        UserID = user.Id,
            //        Pictures = new List<Picture>(),
            //        IsOwner = false,
            //    };

            //        userViewModel.IsOwner = true;
            //    var x = context.Pictures.Where(a => a.UserId == userId);

            //    if (x != null)
            //    {
            //        foreach (var c in x)
            //        {
            //            userViewModel.Pictures.Add(c);
            //            foreach (var item in c.Reaction)
            //            {
            //                userViewModel.TotalLikes++;
            //            }               
            //        }
            //    }
            //    TempData["userViewModel"] = userViewModel;
            //    UserViewModel model = userViewModel;
            //    TempData["userViewModel"] =  new UserViewModel {
            //        UserName = userViewModel.UserName,
            //        IsOwner = userViewModel.IsOwner,
            //        Pictures = userViewModel.Pictures,
            //        ProfilePic = userViewModel.ProfilePic,
            //        UserEmail = userViewModel.UserEmail,
            //        TotalLikes = userViewModel.TotalLikes,
            //        UserID = userViewModel.UserID
            //    };

            //    return View(userViewModel);



            var anon = context.Users.Find(s);
            var userViewModel = new UserViewModel
            {
                UserEmail = anon.Email,
                ProfilePic = anon.ProfilePic,
                UserName = anon.Name,
                UserID = anon.Id,
                Pictures = new List<Picture>(),
                IsOwner = false,
            };
            var x = context.Pictures.Where(a => a.UserId == anon.Id);

            if (x != null)
            {
                foreach (var c in x)
                {
                    userViewModel.Pictures.Add(c);
                    foreach (var item in c.Reaction)
                    {
                        userViewModel.TotalLikes++;
                    }

                }

            }


            return View(userViewModel);




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
                        TimeStamp = DateTime.Now,
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
                        TimeStamp = DateTime.Now,
                        UserId = userId,
                        Hashtag = tag
                    };
                    context.Pictures.Add(newImage);
                    context.SaveChanges();


                }
            }

            return View();
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult UpdateProfilePicture()
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);
            var imageUrl = Request.Form.Get("imageUrl");

            user.ProfilePic = imageUrl;
            context.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]

        public ActionResult Edit(string id)
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);

            if (user == null)
            {
                return new HttpNotFoundResult();
            }

            var model = new UserViewModel
            {
                UserID = user.Id,
                UserEmail = user.Email,
                ProfilePic = user.ProfilePic,
            };
            return View(model);
        }
        public ActionResult GetView()
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);

            var userViewModel = new UserViewModel
            {
                UserEmail = user.Email,
                ProfilePic = user.ProfilePic,
                UserName = user.UserName,
                UserID = user.Id,
                Pictures = new List<Picture>(),

            };

            var x = context.Pictures.Where(a => a.UserId == userId);
            foreach (var c in x)
            {
                userViewModel.Pictures.Add(c);
            }
            return PartialView(userViewModel);
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                var user = context.Users.Find(id);

                if (user == null)
                {
                    return new HttpNotFoundResult();
                }
                if (TryUpdateModel(user, "",
                     new string[] { "UserName", "ProfilePic", "Email" }))
                {
                    var model = new ApplicationUser
                    {
                        Id = user.Id,

                        ProfilePic = user.ProfilePic,

                    };
                }
                context.Entry(user).State = EntityState.Modified;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public void TotalLikes()
        {

        }
    }
}
