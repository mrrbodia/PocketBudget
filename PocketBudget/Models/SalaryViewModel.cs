using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PocketBudget.Models
{
    public class SalaryViewModel
    {
        [Display(Name = "Заробітня плата")]
        public IList<SalaryPeriodViewModel> SalaryPeriods { get; set; }
    }
}