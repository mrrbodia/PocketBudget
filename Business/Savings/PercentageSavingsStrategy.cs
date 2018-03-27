using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Savings
{
    public class PercentageSavingsStrategy : SavingsStrategy
    {
        public override decimal GetSavingsLineAmount(PathModel path, int year)
        {
            return (path.Savings.Amount / 100) * path.Salary.GetCurrentSalary(year + path.CurrentAge) * 12;
        }
    }
}
