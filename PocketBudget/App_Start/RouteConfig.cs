using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PocketBudget
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "v2",
                url: "v2",
                defaults: new { controller = "Home", action = "v2" }
            );

            routes.MapRoute(
                name: "EditFinances",
                url: "editfinances",
                defaults: new { controller = "Home", action = "EditFinances" }
            );

            routes.MapRoute(
                name: "Test",
                url: "test",
                defaults: new { controller = "Home", action = "Test" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}