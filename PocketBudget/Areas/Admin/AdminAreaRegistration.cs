using System.Web.Mvc;

namespace PocketBudget.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_BankDelete",
                "admin/bank/delete/{id}",
                new { controller = "Bank", action = "Delete", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_Banks",
                "admin/bank/list",
                new { controller = "Bank", action = "Index" }
            );

            context.MapRoute(
                "Admin_CreateBank",
                "admin/bank/create",
                new { controller = "Bank", action = "Create" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
