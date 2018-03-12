using AutoMapper;
using Business.Models;
using PocketBudget.Models;
using System.Collections.Generic;
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
            var model = new PathViewModel();
            model.CurrentAge = 20;
            model.LifeExpectancy = 80;
            model.RetirementAge = 60;
            model.Savings.Amount = 5000;
            model.Savings.Type = SavingsType.Fixed;
            model.Spendings.Amount = 10000;
            model.Spendings.Type = SpendingsType.Fixed;
            model.Salary.Amount = 7000;
            model.Pension.Amount = 3000;
            return View(model);
        }

        [HttpPost]
        public ActionResult GetChartLines(PathViewModel pathModel)
        {
            if (pathModel.IsValid())
            {
                var path = mapper.Map<PathViewModel, PathModel>(pathModel);
                var chartLines = PersonalFinances.Chart.GetChartLines(path);
                return Json(chartLines);
            }
            return Json(0);
        }

        //TODO: Create session (save chosen user data)
        [HttpPost]
        public ActionResult EditFinances(int? fromAge)
        {
            //TODO: var model = new AdditionalPathViewModel
            var model = new AdditionalFinancesViewModel();
            model.Deposits = CreateDepositModel();
            model.FromAge = fromAge;
            return View("_EditFinances", model);
        }

        protected IEnumerable<DepositViewModel> CreateDepositModel()
        {
            var result = new List<DepositViewModel>();
            result.Add(new DepositViewModel() { CurrencyId = "hrn", Percentage = 14.0f, Total = 100000m, Years = 1, IsActive = true });
            result.Add(new DepositViewModel() { CurrencyId = "dollar", Percentage = 3.75f, Total = 4000m, Years = 1 });
            result.Add(new DepositViewModel() { CurrencyId = "euro", Percentage = 2.35f, Total = 3000m, Years = 1 });
            return result;
        }
    }
}