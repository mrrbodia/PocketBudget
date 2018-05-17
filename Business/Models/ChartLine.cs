using System;
using System.Collections.Generic;

namespace Business
{
    public class ChartLine
    {
        public ChartLine(string type, List<decimal?> points, decimal total, string currencyId, bool isHidden = true, string defaultTitle = null)
        {
            Type = type;
            Points = new List<decimal?>(points);
            IsHidden = isHidden;
            CurrencyId = currencyId;
            Total = total;
            DefaultTitle = defaultTitle;
        }

        public bool IsHidden { get; set; }

        public string Type { get; set; }

        public List<decimal?> Points { get; set; }

        public string CurrencyId { get; set; }

        public decimal Total { get; set; }

        public string DefaultTitle { get; set; }
    }
}