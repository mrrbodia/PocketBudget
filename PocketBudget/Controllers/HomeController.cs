﻿using AutoMapper;
using Business;
using Business.Models;
using PocketBudget.Models;
using PocketBudget.Models.AdditionalCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PocketBudget.Controllers
{
    public class HomeController : FrontendBaseController
    {
        protected IMapper mapper;

        public HomeController()
        {
            mapper = DependencyResolver.Current.GetService<IMapper>();
        }

        public ActionResult Index()
        {
            var defaultModel = PersonalFinances.Path.GetDefaultPathModel();
            var model = mapper.Map<PathModel, PathViewModel>(defaultModel);
            return View(model);
        }

        //TODO: validate AGE
        //TODO: Create session (save chosen user data)
        [HttpPost]
        public ActionResult GetChartLines(PathViewModel pathModel)
        {
            if (pathModel.IsValid())
            {
                var path = mapper.Map<PathViewModel, PathModel>(pathModel);
                var chartLines = PersonalFinances.Chart.GetChartLines(path);
                var chartLinesModel = mapper.Map<List<ChartLine>, List<ChartLineViewModel>>(chartLines);

                return Json(chartLinesModel);
            }
            return Json(0);
        }

        [HttpGet]
        public ActionResult EditFinances(int? fromAge)
        {
            var model = new AdditionalPathViewModel();
            model.AdditionalIncome = CreateAdditionalIncomeViewModel(fromAge);
            model.AdditionalCost = CreateAdditionalCostViewModel(fromAge);
            return View("_EditFinances", model);
        }

        [HttpPost]
        public ActionResult GetSalaryPeriod(PathViewModel path)
        {
            if (ModelState.IsValid)
            {
                var lastPeriod = path.Salary.SalaryPeriods.Last();
                var salaryPeriod = new SalaryPeriodViewModel(lastPeriod.Amount, lastPeriod.From);
                salaryPeriod.From++;
                path.Salary.SalaryPeriods.Add(salaryPeriod);
            }
            return View("Index", path);
        }

        [HttpPost]
        public ActionResult DeleteSalaryPeriod(PathViewModel path)
        {
            if (ModelState.IsValid && path.Salary.SalaryPeriods.Count > 1)
            {
                path.Salary.SalaryPeriods.RemoveAt(path.Salary.SalaryPeriods.Count - 1);
            }
            return View("Index", path);
        }

        protected AdditionalIncomeViewModel CreateAdditionalIncomeViewModel(int? fromAge)
        {
            var result = new AdditionalIncomeViewModel();
            result.From = fromAge;
            result.Deposits = CreateDepositsModel();
            result.Sales = CreateSalesModel();
            return result;
        }

        protected AdditionalCostViewModel CreateAdditionalCostViewModel(int? fromAge)
        {
            var result = new AdditionalCostViewModel();
            result.From = fromAge;
            result.Credits = CreateCreditsModel();
            result.Purchases = CreatePurchasesModel();
            return result;
        }

        protected IList<PurchaseViewModel> CreatePurchasesModel()
        {
            var result = new List<PurchaseViewModel>();
            result.Add(new PurchaseViewModel() { CurrencyId = Constants.Currency.Hrn, Total = 100000m, IsActive = true });
            result.Add(new PurchaseViewModel() { CurrencyId = Constants.Currency.Dollar, Total = 4000m });
            result.Add(new PurchaseViewModel() { CurrencyId = Constants.Currency.Euro, Total = 3000m });
            return result;
        }

        protected IList<DepositViewModel> CreateDepositsModel()
        {
            var result = new List<DepositViewModel>();
            result.Add(new DepositViewModel() { CurrencyId = Constants.Currency.Hrn, Percentage = 14.0f, Total = 100000m, Years = 1, IsActive = true });
            result.Add(new DepositViewModel() { CurrencyId = Constants.Currency.Dollar, Percentage = 3.75f, Total = 4000m, Years = 1 });
            result.Add(new DepositViewModel() { CurrencyId = Constants.Currency.Euro, Percentage = 2.35f, Total = 3000m, Years = 1 });
            return result;
        }

        protected IList<SaleViewModel> CreateSalesModel()
        {
            var result = new List<SaleViewModel>();
            result.Add(new SaleViewModel() { CurrencyId = Constants.Currency.Hrn, Total = 100000m, IsActive = true });
            result.Add(new SaleViewModel() { CurrencyId = Constants.Currency.Dollar, Total = 4000m });
            result.Add(new SaleViewModel() { CurrencyId = Constants.Currency.Euro, Total = 3000m });
            return result;
        }

        protected IList<CreditViewModel> CreateCreditsModel()
        {
            var result = new List<CreditViewModel>();
            result.Add(new CreditViewModel() { CurrencyId = Constants.Currency.Hrn, Percentage = 25.0f, Total = 100000m, Years = 2, IsActive = true });
            result.Add(new CreditViewModel() { CurrencyId = Constants.Currency.Dollar, Percentage = 8.0f, Total = 4000m, Years = 2 });
            result.Add(new CreditViewModel() { CurrencyId = Constants.Currency.Euro, Percentage = 5.0f, Total = 3000m, Years = 2 });
            return result;
        }
    }
}