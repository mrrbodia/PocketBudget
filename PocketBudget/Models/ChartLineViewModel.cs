using Business;
using PocketBudget.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class ChartLineViewModel
    {
        public string Type { get; set; }

        public bool IsHidden { get; set; }

        public List<decimal?> Points { get; set; }

        public string CurrencyId { get; set; }

        public decimal Total { get; set; }

        public string DefaultTitle { get; set; }
        //TODO: ????
        public string Title
        {
            get
            {
                return GetChartTitlePerType();
            }
        }

        public string GetChartTitlePerType()
        {
            var currencySymbol = ViewHelper.GetCurrencySymbol(this.CurrencyId);
            switch (Type)
            {
                case Constants.ChartLineType.Credit:
                    return string.Format("Кредит: {0}{1}", currencySymbol, this.Total);
                case Constants.ChartLineType.Deposit:
                    return string.Format("Депозит: {0}{1}", currencySymbol, this.Total);
                case Constants.ChartLineType.Purchase:
                    return string.Format("Купівля: {0}{1}", currencySymbol, this.Total);
                case Constants.ChartLineType.Sale:
                    return string.Format("Продаж: {0}{1}", currencySymbol, this.Total);
                case Constants.ChartLineType.Education:
                    return string.Format("Освіта: {0}", DefaultTitle);
                default:
                    return string.Format("Накопичення: {0}{1}/міс", currencySymbol, this.Total);
            }
        }
    }
}