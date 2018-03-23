using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Spendings
{
    public class FixedSpendingsStrategy : SpendingsStrategy
    {
        public override decimal GetSpendingsLineAmount(PathModel path, decimal? savedMoney)
        {
            return path.Pension.Amount * 12 - path.Spendings.Amount * 12;
        }
    }
}
