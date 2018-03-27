using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Savings
{
    public class FixedSavingsStrategy : SavingsStrategy
    {
        public override decimal GetSavingsLineAmount(PathModel path, int year)
        {
            return path.Savings.Amount * 12;
        }
    }
}
