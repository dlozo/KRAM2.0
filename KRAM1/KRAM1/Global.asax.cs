using KRAM1.App_Start;
using KRAM1.Models;
using KRAM1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KRAM1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
        protected void Session_Start()
        {
            Session["UserIsAdmin"] = false;
            ApplicationDbContext context = new ApplicationDbContext();
            foreach (var user in context.Users.ToList())
            {
                if (user.IsAdmin == true)
                {
                    Session["UserIsAdmin"] = true;
                }
            }
        }
        private void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("exception", exception);

            if (exception.GetType() == typeof(HttpException))
            {
                routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
            }
            else
            {
                routeData.Values.Add("statusCode", 500);
            }

            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Response.End();

        }
    }


    }

