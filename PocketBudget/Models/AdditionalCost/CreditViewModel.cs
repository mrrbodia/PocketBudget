using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models.AdditionalCost
{
    public class CreditViewModel : AdditionalCostItemViewModel
    {
        public float Percentage { get; set; }

        public short Years { get; set; }

        public override string Title
        {
            get
            {
                return string.Format("Кредит в {0} {1:0.00}% річних", CurrencySymbol(CurrencyId), Percentage);
            }
        }
    }
}