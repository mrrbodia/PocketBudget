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
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update()//model
        { 
            return View();
        }

        public ActionResult Delete(string id)
        {
            return View();
        }

    }
}
