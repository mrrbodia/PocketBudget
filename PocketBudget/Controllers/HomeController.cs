﻿using PocketBudget;
using PocketBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocketBudget.Controllers
{
    public class HomeController : FrontendBaseController
    {
        //public ActionResult Index()
        //{
        //    var model = new StrategyViewModel();
        //    model.SalaryPattern = CreateSalaryPatternModel();
        //    return View(model);
        //}

        //public ActionResult Test()
        //{
        //    var model = new StrategyViewModel();
        //    model.SalaryPattern = CreateSalaryPatternModel();
        //    return View(model);
        //}

        public ActionResult Index()
        {
            var model = new PathViewModel();
            var banks = PersonalFinances.Bank.GetAll();
            model.CurrentAge = 20;
            model.LifeExpectancy = 80;
            model.RetirementAge = 60;
            model.Savings = 5000;
            model.Spendings = 15000;
            return View(model);
        }

        //TODO: fromAge can be null also
        [HttpPost]
        public ActionResult EditFinances(int fromAge)
        {
            var model = new AdditionalFinancesViewModel();
            model.Deposits = CreateDepositModel();
            model.FromAge = fromAge;
            return View("_EditFinances", model);
        }

        protected IEnumerable<TestDepositModel> CreateDepositModel()
        {
            var result = new List<TestDepositModel>();
            result.Add(new TestDepositModel() { CurrencyId = "hrn", Percentage = 14.0f, Total = 100000m, Years = 1, IsActive = true });
            result.Add(new TestDepositModel() { CurrencyId = "dollar", Percentage = 3.75f, Total = 4000m, Years = 1 });
            result.Add(new TestDepositModel() { CurrencyId = "euro", Percentage = 2.35f, Total = 3000m, Years = 1 });
            return result;
        }

        protected TestSalaryPatternModel CreateSalaryPatternModel()
        {
            var result = new TestSalaryPatternModel();
            result.IncomePerYear = 60000m;
            result.IncreaseTillAge = 45;
            result.IncreasePercentage = 3.0;
            result.ShowTillAge = 80;
            result.CurrentAge = 18;
            result.RetirementAge = 65;
            return result;
        }
    }
}