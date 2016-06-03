using KRAM1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRAM1.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index(int imageId)
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);
            
            if (user.IsAdmin == true)
            {
                var currentImage = context.Pictures.Find(imageId);
                context.Pictures.Remove(currentImage);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}