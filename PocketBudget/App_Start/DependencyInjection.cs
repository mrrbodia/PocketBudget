using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Business.Managers;
using Business.DataProviders;
using Business.Managers.Chart;
using Business.Components.AdditionalPath;
using PocketBudget.Web.Mvc;
using AutoMapper;
using PocketBudget.Models;
using Business.Models;

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
            builder.RegisterType<ChartManager>().As<IChartManager>();
            builder.RegisterType<PathModelBinder>().As<PathModelBinder>();

            RegisterAdditionalProcessor(builder);
            RegisterMapper(builder);
        }

        protected static void RegisterMapper(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetMapper());
        }

        protected static void RegisterAdditionalProcessor(ContainerBuilder builder)
        {
            builder.RegisterType<AdditionalSavingsProcessor>().As<AdditionalSavingsProcessor>();

            builder.RegisterType<DepositIncomeStep>().As<IAdditionalIncomeStep>();
        }

        protected static IMapper GetMapper()
        {
            return new MapperConfiguration(x =>
            {
                x.CreateMap<PathViewModel, PathModel>();
                x.CreateMap<PathModel, PathViewModel>();
            }).CreateMapper();
        }
    }
}