using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public interface IAdditionalIncome
    {
        short From { get; set; }

        short To { get; set; }

        decimal Total { get; set; }

        string CurrencyId { get; set; }

        bool IsActive { get; set; }

        bool IsHidden { get; set; }

        decimal GetIncomePerYear(int currentYear);

        string LineType { get; }
    }

    public abstract class AdditionalIncome : IAdditionalIncome
    {
        public virtual short From { get; set; }

        public virtual short To { get; set; }

        public virtual decimal Total { get; set; }

        public virtual string CurrencyId { get; set; }

        public bool IsActive { get; set; }

        public bool IsHidden { get; set; }

        public abstract string LineType { get; }

        public abstract decimal GetIncomePerYear(int currentYear);

        protected virtual decimal GetCurrencyExchangeValue()
        {
            switch (CurrencyId)
            {
                case Constants.Currency.Dollar:
                    return 26.00m;
                case Constants.Currency.Euro:
                    return 31.00m;
                case Constants.Currency.Hrn:
                    return 1.0m;
                default:
                    return 1.0m;
            }
        }
    }
}
