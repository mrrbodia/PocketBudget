using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Web
{
    public static class ViewHelper
    {
        public static string GetCurrencySymbol(string currencyId)
        {
            switch (currencyId)
            {
                case Constants.Currency.Euro:
                    return Constants.Currency.EuroSymbol;
                case Constants.Currency.Dollar:
                    return Constants.Currency.DollarSymbol;
                case Constants.Symbols.Percent:
                    return Constants.Symbols.Percent;
                default:
                    return Constants.Currency.HrnSymbol;
            }
        }
    }
}