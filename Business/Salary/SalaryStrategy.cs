using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Salary
{
    public class SalaryStrategy : ISalaryStrategy
    {
        protected ISalaryStrategy strategy;

        public SalaryStrategy(ISalaryStrategy strategy)
        {
            this.strategy = strategy;
        }

        public decimal GetIncomePerYear()
        {
            return this.strategy.GetIncomePerYear();
        }
    }
}
