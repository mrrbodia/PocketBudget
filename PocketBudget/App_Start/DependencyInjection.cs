using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Business.Managers;
using Business.Managers.Chart;
using Business.Components.AdditionalPath;
using AutoMapper;
using PocketBudget.Models;
using Business.Models;
using Business.DomainModel.Active;

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
                x.CreateMap<SalaryModel, SalaryViewModel>();
                x.CreateMap<SalaryViewModel, SalaryModel>();
                x.CreateMap<SavingsModel, SavingsViewModel>();
                x.CreateMap<SavingsViewModel, SavingsModel>();
                x.CreateMap<PensionViewModel, PensionModel>();
                x.CreateMap<PensionModel, PensionViewModel>();
                x.CreateMap<SpendingsViewModel, SpendingsModel>();
                x.CreateMap<SpendingsModel, SpendingsViewModel>();
                x.CreateMap<AdditionalIncomeViewModel, AdditionalIncome>();
                x.CreateMap<AdditionalIncome, AdditionalIncomeViewModel>();
                x.CreateMap<AdditionalPathViewModel, AdditionalPathModel>();
                x.CreateMap<AdditionalPathModel, AdditionalPathViewModel>();
                x.CreateMap<DepositViewModel, Deposit>();
                x.CreateMap<Deposit, DepositViewModel>();
            }).CreateMapper();
        }
    }
}