using Business.DomainModel.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Cost
{
    public class Credit : AdditionalCost
    {
        public virtual double Percentage { get; set; }
        
        public virtual short Years { get; set; }

        public override string LineType => Constants.ChartLineType.Credit;

        public override decimal GetCostPerYear()
        {
            return Total * (decimal)(Percentage / 100) * -GetCurrencyExchangeValue();
        }
    }
}