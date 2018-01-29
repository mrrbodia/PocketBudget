using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class AdditionalFinancesViewModel
    {
        public int FromAge { get; set; }

        public short ToAge { get; set; }

        public IEnumerable<DepositModel> Deposits { get; set; }
    }
}