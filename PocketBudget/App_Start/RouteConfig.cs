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
                name: "ClearSession",
                url: "clear",
                defaults: new { controller = "Home", action = "ClearPath" }
            );

            routes.MapRoute(
                name: "EditFinances",
                url: "editfinances",
                defaults: new { controller = "Home", action = "EditFinances" }
            );

            routes.MapRoute(
                name: "GetSalaryPeriod",
                url: "getsalaryperiod",
                defaults: new { controller = "Salary", action = "GetSalaryPeriod" }
            );

            routes.MapRoute(
                name: "DeleteSalaryPeriod",
                url: "deletesalaryperiod",
                defaults: new { controller = "Salary", action = "DeleteSalaryPeriod" }
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