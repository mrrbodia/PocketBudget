using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class DepositModel
    {
        public float Percentage { get; set; }

        public decimal Total { get; set; }

        public string CurrencyId { get; set; }

        public short Years { get; set; }

        public short FromAge { get; set; }

        public bool IsActive { get; set; }
    }
}