using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public interface IAdditionalIncome
    {
        short From { get; set; }

        short To { get; set; }
        
        decimal GetIncomePerYear(int currentYear);
    }
}
