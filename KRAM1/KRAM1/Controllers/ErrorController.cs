using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRAM1.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            return View();
        }
    }
}