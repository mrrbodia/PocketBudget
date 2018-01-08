using Business.DomainModel.Active;
using PocketBudget.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocketBudget.Areas.Admin.Controllers
{
    public class BankController : Controller
    {
        public ActionResult Index()
        {
            var banks = PersonalFinances.Bank.GetAll();
            var model = banks.Select(x => CreateBankModel(x)).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new BankViewModel();
            model.Id = Guid.NewGuid().ToString();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BankViewModel model)
        {
            var bank = new Bank();
            bank.Deposits = new List<Deposit>();
            bank.Id = model.Id;
            bank.Name = model.Name;
            bank.Rating = model.Rating;
            PersonalFinances.Bank.Create(bank);
            return RedirectToRoute("Admin_Banks");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            PersonalFinances.Bank.Delete(id);
            return RedirectToRoute("Admin_Banks");
        }

        protected BankViewModel CreateBankModel(Bank bank)
        {
            var result = new BankViewModel();
            result.Id = bank.Id;
            result.Name = bank.Name;
            result.Rating = bank.Rating;
            return result;
        }

    }
}
