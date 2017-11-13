using PocketBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new StrategyViewModel();
            model.SalaryPattern = CreateTestSalaryPatternModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetDataForStrategy(StrategyViewModel model)
        {
            
            return Json(1);
        }

        protected TestSalaryPatternModel CreateTestSalaryPatternModel()
        {
            var result = new TestSalaryPatternModel();
            result.IncomePerYear = 60000m;
            result.IncreaseTillAge = 45;
            result.IncreasePercentage = 3.0;
            result.StartWorkFrom = 18;
            result.ShowTillAge = 65;
            return result;
        }
    }
}