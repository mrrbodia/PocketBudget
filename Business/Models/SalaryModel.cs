using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class SalaryModel
    {
        public IList<SalaryPeriod> SalaryPeriods { get; set; }

        public decimal GetCurrentSalary(int year)
        {
            return SalaryPeriods?.FirstOrDefault(x => x.From <= year && x.To >= year)?.Amount ?? 0;
        }
    }
}
