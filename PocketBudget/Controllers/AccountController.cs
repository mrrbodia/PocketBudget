using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocketBudget.Controllers
{
    public class AccountController : FrontendBaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            PersonalFinances.Account.Register(new Business.DomainModel.Account.Account());
            return View();
        }

        public ActionResult Login()
        {
            PersonalFinances.Account.Login();
            return View();
        }

        public ActionResult Logout()
        {
            PersonalFinances.Account.Logout();
            return View();
        }

    }
}
