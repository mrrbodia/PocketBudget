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
using System.Collections.Generic;
using Business.DomainModel.Cost;
using PocketBudget.Models.AdditionalCost;
using Business;
using Business.Managers.Path;

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
            builder.RegisterType<AccountManager>().As<IAccountManager>();
            builder.RegisterType<BankManager>().As<IBankManager>();
            builder.RegisterType<ChartManager>().As<IChartManager>();
            builder.RegisterType<PathManager>().As<IPathManager>();

            RegisterAdditionalProcessor(builder);
            RegisterMapper(builder);
        }

        protected static void RegisterMapper(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetMapper());
        }

        protected static void RegisterAdditionalProcessor(ContainerBuilder builder)
        {
            builder.RegisterType<AdditionalPathProcessor>().As<AdditionalPathProcessor>();
            builder.RegisterType<DepositIncomeStep>().As<IAdditionalIncomeStep>();
            builder.RegisterType<SaleIncomeStep>().As<IAdditionalIncomeStep>();

            builder.RegisterType<CreditCostStep>().As<IAdditionalCostStep>();
            builder.RegisterType<PurchaseCostStep>().As<IAdditionalCostStep>();
        }

        protected static IMapper GetMapper()
        {
            return new MapperConfiguration(x =>
            {
                x.CreateMap<PathViewModel, PathModel>();
                x.CreateMap<PathModel, PathViewModel>();
                x.CreateMap<SalaryModel, SalaryViewModel>();
                x.CreateMap<SalaryViewModel, SalaryModel>();
                x.CreateMap<SalaryPeriod, SalaryPeriodViewModel>();
                x.CreateMap<SalaryPeriodViewModel, SalaryPeriod>();
                x.CreateMap<SavingsModel, SavingsViewModel>();
                x.CreateMap<SavingsViewModel, SavingsModel>();
                x.CreateMap<PensionViewModel, PensionModel>();
                x.CreateMap<PensionModel, PensionViewModel>();
                x.CreateMap<SpendingsViewModel, SpendingsModel>();
                x.CreateMap<SpendingsModel, SpendingsViewModel>();
                x.CreateMap<AdditionalPathViewModel, AdditionalPathModel>()
                        .ForMember(dest => dest.AdditionalIncomes,
                                   opts => opts.ResolveUsing(new AdditionalIncomeResolver()))
                        .ForMember(dest => dest.AdditionalCosts,
                                   opts => opts.ResolveUsing(new AdditionalCostResolver()));
                x.CreateMap<AdditionalPathModel, AdditionalPathViewModel>()
                        .ForMember(dest => dest.AdditionalIncome,
                                   opts => opts.MapFrom(
                                       src => new AdditionalIncomeViewModel()));
                x.CreateMap<DepositViewModel, Deposit>();
                x.CreateMap<Deposit, DepositViewModel>();
                x.CreateMap<SaleViewModel, Sale>();
                x.CreateMap<Sale, SaleViewModel>();
                x.CreateMap<Credit, CreditViewModel>();
                x.CreateMap<CreditViewModel, Credit>();
                x.CreateMap<Purchase, PurchaseViewModel>();
                x.CreateMap<PurchaseViewModel, Purchase>();
                x.CreateMap<ChartLine, ChartLineViewModel>();
                x.CreateMap<ChartLineViewModel, ChartLine>();
            }).CreateMapper();
        }
    }
}