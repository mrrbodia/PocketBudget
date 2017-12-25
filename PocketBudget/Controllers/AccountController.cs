using Business.DomainModel.Account;
using PocketBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PocketBudget.Controllers
{
    public class AccountController : FrontendBaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            var model = new AccountViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(AccountViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var existingAccount = PersonalFinances.Account.GetByEmail(model.Email);
            if (existingAccount != null)
                return View(model);
            var account = new Account() { Email = model.Email, Password = model.Password, Role = Role.User };
            PersonalFinances.Account.Register(account);
            FormsAuthentication.SetAuthCookie(model.Email, false);
            return RedirectToRoute("Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Request?.RequestContext?.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
                return RedirectToRoute("Home");

            return View(); 
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            FormsAuthentication.SetAuthCookie(model.Email, false);
            return RedirectToRoute("Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }

    }
}
