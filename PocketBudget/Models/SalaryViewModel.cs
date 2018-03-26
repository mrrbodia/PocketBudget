using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PocketBudget.Models
{
    public class SalaryViewModel
    {
        public IList<SalaryPeriodViewModel> SalaryPeriods { get; set; }
    }
}