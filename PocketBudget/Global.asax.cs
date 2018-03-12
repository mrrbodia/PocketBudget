﻿using Business.Models;
using PocketBudget.App_Start;
using PocketBudget.Web.Mvc;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PocketBudget
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyInjection.RegisterDependencies();
            RegisterMvc();
        }

        protected void RegisterMvc()
        {
            ModelBinders.Binders.Add(typeof(PathModel), DependencyResolver.Current.GetService<PathModelBinder>());
        }
    }
}