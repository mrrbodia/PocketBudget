using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class AdditionalIncomeViewModel
    {
        public int From { get; set; }

        public int To { get; set; }

        public DepositViewModel Deposit { get; set; }
    }
}