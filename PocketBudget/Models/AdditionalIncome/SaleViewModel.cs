using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class SaleViewModel : AdditionalIncomeItemViewModel
    {
        public override string Title
        {
            get
            {
                return string.Format("Продаж в {0}", CurrencySymbol(CurrencyId));
            }
        }
    }
}