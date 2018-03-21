using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models.AdditionalCost
{
    public class PurchaseViewModel : AdditionalCostItemViewModel
    {
        public override string Title
        {
            get
            {
                return string.Format("Покупка в {0}", CurrencySymbol(CurrencyId));
            }
        }
    }
}