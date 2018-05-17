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
            var model = PersonalFinances.Path.GetDefaultPathModel();
            var viewModel = mapper.Map<PathModel, PathViewModel>(model);
            viewModel.ProfessionSelection.Professions = CreateProfessionsModel();
            viewModel.EducationDegrees = CreateEducationDegreesModel();
            return View(viewModel);
        }

        protected EducationDegreesViewModel CreateEducationDegreesModel()
        {
            //TODO: Move to XML
            var result = new EducationDegreesViewModel();
            result.Degrees =  new List<EducationDegreeViewModel>();
            //result.Degrees.Add(new EducationDegreeViewModel() { Title = "Школа", IsReached = true, ReachedIn = 14, MinReachAge = 14, IncomePercent = 0 });
            return result;
        }

        protected IList<ProfessionViewModel> CreateProfessionsModel()
        {
            //TODO: Move to XML
            var result = new List<ProfessionViewModel>();
            result.Add(new ProfessionViewModel() { Title = "Не обрано", IsSelected = true, Id = "0" });
            result.Add(new ProfessionViewModel() { Title = "Викладач", IsSelected = false, Id = "1" });
            result.Add(new ProfessionViewModel() { Title = "Програміст", IsSelected = false, Id = "2" });
            return result;
        }

        [HttpPost]
        public ActionResult GetPathModel(string modelId)
        {
            var model = PersonalFinances.Path.GetPathModel(modelId);
            if(model != null)
            {
                var chartLines = GetChartLines(model);
                return Json(new { lines = chartLines, model = model });
            }
            return Json(0);
        }
        
        //TODO: Create session (save chosen user data)
        [HttpPost]
        public ActionResult GetChartLines(PathViewModel pathModel)
        {
            if (pathModel.IsValid())
            {
                var path = GetPathModel(pathModel);
                var chartLines = GetChartLines(path);
                return Json(chartLines);
            }
            return Json(0);
        }

        protected PathModel GetPathModel(PathViewModel pathModel)
        {
            var result = mapper.Map<PathViewModel, PathModel>(pathModel);
            if (pathModel.EducationDegrees?.Degrees?.Any(x => x.IsReached) ?? false)
            {
                var highestDegree = pathModel.EducationDegrees.Degrees.LastOrDefault(x => x.IsReached);
                result.Education = new EducationModel(highestDegree.Title, highestDegree.ReachedIn, highestDegree.IncomePercent);
            }
            return result;
        }

        protected virtual List<ChartLineViewModel> GetChartLines(PathModel path)
        {
            var chartLines = PersonalFinances.Chart.GetChartLines(path);
            var chartLinesModel = mapper.Map<List<ChartLine>, List<ChartLineViewModel>>(chartLines);
            return chartLinesModel;
        }

        [HttpGet]
        public ActionResult EditFinances(int? fromAge)
        {
            var model = new AdditionalPathViewModel();
            model.AdditionalIncome = CreateAdditionalIncomeViewModel(fromAge);
            model.AdditionalCost = CreateAdditionalCostViewModel(fromAge);
            return View("_EditFinances", model);
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