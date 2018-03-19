using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Cost
{
    public class Purchase : AdditionalCost
    {
        public override decimal GetCostPerYear()
        {
            return Total * -GetCurrencyExchangeValue(); ;
        }
    }
}
