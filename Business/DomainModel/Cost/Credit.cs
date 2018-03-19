using Business.DomainModel.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Cost
{
    public class Credit : IAdditionalCost
    {
        public virtual double Percentage { get; set; }

        public virtual decimal Total { get; set; }

        public virtual string CurrencyId { get; set; }
        
        public virtual short Years { get; set; }

        public virtual short From { get; set; }

        public virtual short To { get; set; }
        
        public virtual decimal GetCostPerYear()
        {
            return Total * (decimal)(Percentage / 100) * -GetCurrencyExchangeValue();
        }

        //TODO: Replace with better solution
        protected decimal GetCurrencyExchangeValue()
        {
            switch (CurrencyId)
            {
                case Constants.Currency.Dollar:
                    return 27.95m;
                case Constants.Currency.Euro:
                    return 31.15m;
                case Constants.Currency.Hrn:
                    return 1.0m;
                default:
                    return 1.0m;

            }
        }
    }
}