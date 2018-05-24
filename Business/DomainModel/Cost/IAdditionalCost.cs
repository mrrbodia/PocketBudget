using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business.DomainModel.Cost
{
    public interface IAdditionalCost
    {
        short From { get; set; }

        short To { get; set; }

        decimal Total { get; set; }

        string CurrencyId { get; set; }

        bool IsHidden { get; set; }

        string LineType { get; }

        decimal GetCostPerYear();
    }

    public abstract class AdditionalCost : IAdditionalCost
    {
        public virtual short From { get; set; }

        public virtual short To { get; set; }

        public virtual string CurrencyId { get; set; }

        public virtual decimal Total { get; set; }

        public bool IsHidden { get; set; }

        public abstract string LineType { get; }

        public abstract decimal GetCostPerYear();

        //TODO: Replace with better solution (get from XML)
        protected virtual decimal GetCurrencyExchangeValue()
        {
            if (HttpContext.Current?.Request?.Cookies != null)
            {
                var cookie = HttpContext.Current.Request.Cookies.Get(CurrencyId);
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value) && decimal.TryParse(cookie.Value, out decimal result))
                    return result;
            }
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
