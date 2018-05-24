using PocketBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocketBudget.Controllers
{
    public class SalaryController : Controller
    {
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
            return View("~/Views/Home/Index.cshtml", path);
        }

        [HttpPost]
        public ActionResult DeleteSalaryPeriod(PathViewModel path)
        {
            if (ModelState.IsValid && path.Salary.SalaryPeriods.Count > 1)
            {
                path.Salary.SalaryPeriods.RemoveAt(path.Salary.SalaryPeriods.Count - 1);
            }
            return View("~/Views/Home/Index.cshtml", path);
        }
    }
}
