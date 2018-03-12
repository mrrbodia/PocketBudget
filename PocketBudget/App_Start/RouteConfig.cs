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
                name: "GetChartLines",
                url: "getchartlines",
                defaults: new { controller = "Home", action = "GetChartLines" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "account/logout",
                defaults: new { controller = "Account", action = "Logout" }
            );

            routes.MapRoute(
                name: "Login",
                url: "account/login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Register",
                url: "account/register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "EditFinances",
                url: "editfinances",
                defaults: new { controller = "Home", action = "EditFinances" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}