using Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocketBudget
{
    public class PersonalFinances
    {
        public static IAccountManager Account 
        {
            get
            {
                return DependencyResolver.Current.GetService<IAccountManager>();
            }
        }

        public static IBankManager Bank
        {
            get
            {
                return DependencyResolver.Current.GetService<IBankManager>();
            }
        }
    }
}