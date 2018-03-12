using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    //TODO: Replace with AdditionalPathViewModel
    public class AdditionalFinancesViewModel
    {
        public int? FromAge { get; set; }

        public IEnumerable<DepositViewModel> Deposits { get; set; }
    }
}