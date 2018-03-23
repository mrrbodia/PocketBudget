using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public class Sale : AdditionalIncome
    {
        public override decimal GetIncomePerYear(int currentYear)
        {
            return Total * GetCurrencyExchangeValue();
        }
    }
}