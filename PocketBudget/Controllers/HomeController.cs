﻿using AutoMapper;
using Business;
using Business.DomainModel.Active;
using Business.Models;
using PocketBudget.App_Start;
using PocketBudget.Models;
using PocketBudget.Models.AdditionalCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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

        protected void SetCurrency()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var str = client.UploadString("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5", "");
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var currencies = serializer.Deserialize<List<Currency>>(str);
                    foreach (var item in currencies)
                    {
                        var cookie = new HttpCookie(item.ccy, item.sale);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.Cookies.Add(cookie);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public ActionResult Index()
        {
            SetCurrency();
            var pathModel = Session[Constants.SessionKeys.UserKey] as PathModel;
            if (pathModel != null)
                Session[Constants.SessionKeys.IsCustomizedModel] = true;
            var model = pathModel == null ? PersonalFinances.Path.GetDefaultPathModel() : pathModel;
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
            result.Degrees.Add(new EducationDegreeViewModel() { Title = "Школа", IsReached = false, ReachedIn = 14, MinReachAge = 14, IncomePercent = 0 });
            result.Degrees.Add(new EducationDegreeViewModel() { IsReached = false, MinReachAge = 18, ReachedIn = 18, Title = "Бакалавр", IncomePercent = 0.10m });
            result.Degrees.Add(new EducationDegreeViewModel() { IsReached = false, MinReachAge = 20, ReachedIn = 20, Title = "Магістр", IncomePercent = 0.15m });
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
            var pathModel = Session[modelId] as PathModel;
            var model = pathModel == null ? PersonalFinances.Path.GetPathModel(modelId) : pathModel;
            if (model != null)
            {
                var chartLines = GetChartLines(model);
                return Json(new { lines = chartLines, model = model });
            }
            return Json(0);
        }
        
        [HttpPost]
        public ActionResult GetChartLines(PathViewModel pathModel)
        {
            if (pathModel.IsValid())
            {
                var path = GetPathModel(pathModel);
                var chartLines = GetChartLines(path);
                return Json(new { lines = chartLines, model = path });
            }
            return Json(0);
        }

        [HttpGet]
        public ActionResult ClearPath()
        {
            Session.Remove(Constants.SessionKeys.UserKey);
            return RedirectToRoute("Home");
        }

        protected PathModel GetPathModel(PathViewModel pathModel)
        {
            PathModel result;
            if(Session[Constants.SessionKeys.IsCustomizedModel] != null && bool.Parse(Session[Constants.SessionKeys.IsCustomizedModel].ToString()))
            {
                result = Session[Constants.SessionKeys.UserKey] as PathModel;
                Session[Constants.SessionKeys.IsCustomizedModel] = false;
            }
            else
            {
                result = mapper.Map<PathViewModel, PathModel>(pathModel);
                result.Education = new EducationModel(true);
            }
            if (pathModel.EducationDegrees?.Degrees?.Any(x => x.IsReached) ?? false)
            {
                result.Education = new EducationModel(pathModel.EducationDegrees.IsHidden);
                result.Education.EducationDegrees = new List<EducationDegreeModel>();
                foreach (var degree in pathModel.EducationDegrees.Degrees.Where(x => x.IsReached))
                {
                    result.Education.EducationDegrees.Add(new EducationDegreeModel(degree.Title, degree.ReachedIn, degree.IncomePercent));
                }
            }
            if (result?.Id?.Equals(Constants.SessionKeys.UserKey) ?? false)
                Session[Constants.SessionKeys.UserKey] = result;
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