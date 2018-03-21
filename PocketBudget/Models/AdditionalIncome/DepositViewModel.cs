using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class DepositViewModel : AdditionalIncomeItemViewModel
    {
        public float Percentage { get; set; }

        public short Years { get; set; }

        public override string Title
        {
            get
            {
                return string.Format("Депозит в {0} {1:0.00}% річних", CurrencySymbol(CurrencyId), Percentage);
            }
        }
    }
}