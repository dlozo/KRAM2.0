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
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);

            var userViewModel = new UserViewModel
                {
                UserEmail=user.Email,
                ProfilePic=user.ProfilePic,
                UserName=user.UserName,
                UserID=user.Id,
                Pictures = user.UploadedPicId,
                //TotalLikes = user.
            };

            return View(userViewModel);
          
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
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id) 
        {
          var user = context.Users.Find(id);
               
    if (user == null)
    {
        return new HttpNotFoundResult();
    }

            var model = new UserViewModel
                {
                UserID=user.Id,
                UserEmail=user.Email,
                ProfilePic=user.ProfilePic,
               
                };
             
    return View(model);


           
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {    var user = context.Users.Find(id);
               
             if (user == null)
               {
        return new HttpNotFoundResult();
             }
             if (TryUpdateModel(user, "",
                  new string[] { "UserName", "ProfilePic", "Email" }))
                    { 
                var model = new ApplicationUser
                    {
                    Id=user.Id,
                  
                    ProfilePic=user.ProfilePic,
                   
                };}
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
    }
}
