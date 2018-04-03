using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public class Deposit : AdditionalIncome
    {
        public virtual double Percentage { get; set; }
        
        public virtual short Years { get; set; }

        public override string LineType => Constants.ChartLineType.Deposit;

        public override decimal GetIncomePerYear(int currentYear)
        {
            return (Total * (decimal)Math.Pow((1 + Percentage / 100), currentYear) - Total) * GetCurrencyExchangeValue();
        }
    }
}