using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class StrategyViewModel
    {
        public Cost Cost { get; set; }

        public DepositCurrency DepositCurrency { get; set; }
    }
}