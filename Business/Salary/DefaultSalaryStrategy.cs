using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Salary
{
    public class DefaultSalaryStrategy : ISalaryStrategy
    {
        public decimal GetIncomePerYear()
        {
            return 0;
        }
    }
}
