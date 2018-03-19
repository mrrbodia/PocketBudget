using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public class Deposit : IAdditionalIncome
    {
        public virtual double Percentage { get; set; }

        public virtual decimal Total { get; set; }

        public virtual string CurrencyId { get; set; }
        
        public virtual short Years { get; set; }

        public virtual short From { get; set; }

        public virtual short To { get; set; }
        
        public virtual decimal GetIncomePerYear(int currentYear)
        {
            //TODO: when deposit for 1 year is selected - no income present
            return (Total * (decimal)Math.Pow((1 + Percentage / 100), currentYear) - Total) * GetCurrencyExchangeValue();
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