using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBudget.Models
{
    public abstract class AdditionalCostItemViewModel
    {
        public virtual decimal Total { get; set; }

        public virtual string CurrencyId { get; set; }

        public virtual short FromAge { get; set; }

        public virtual bool IsActive { get; set; }

        public abstract string Title { get; }

        public virtual string CurrencySymbol(string currencyId)
        {
            switch (currencyId)
            {
                case Constants.Currency.Euro:
                    return Constants.Currency.EuroSymbol;
                case Constants.Currency.Dollar:
                    return Constants.Currency.DollarSymbol;
                default:
                    return Constants.Currency.HrnSymbol;
            }
        }
    }
}
