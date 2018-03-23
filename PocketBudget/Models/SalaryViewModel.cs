using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class SalaryViewModel
    {
        [Display(Name = "Заробітня плата / місяць")]
        public decimal Amount { get; set; }
    }
}