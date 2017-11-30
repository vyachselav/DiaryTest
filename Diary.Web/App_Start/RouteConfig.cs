using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Diary.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Notes", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Pictures",
                url: "Pictures/{action}/{id}",
                defaults: new {action = "Index"}
            );

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}",
                defaults: new { action = "Login"}
            );
        }
    }
}
