using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Spendings
{
    public class PercentageSpendingsStrategy : SpendingsStrategy
    {
        public override decimal GetSpendingsLineAmount(PathModel path, decimal? savedMoney)
        {
            if (savedMoney.HasValue)
                return (-path.Spendings.Amount / 100) * savedMoney.Value;
            return 0;
        }
    }
}
