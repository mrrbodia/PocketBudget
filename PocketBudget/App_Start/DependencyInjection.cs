using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Business.Managers;
using Business.DataProviders;

namespace PocketBudget.App_Start
{
    public class DependencyInjection
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(DependencyInjection).Assembly);
            RegisterTypes(builder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        protected static void RegisterTypes(ContainerBuilder builder)
        {
            //Register managers instances
            builder.RegisterType<AccountManager>().As<IAccountManager>();
            builder.RegisterType<BankManager>().As<IBankManager>();

        }
    }
}