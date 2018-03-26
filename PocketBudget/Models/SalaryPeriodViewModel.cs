using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class SalaryPeriodViewModel
    {
        [Display(Name = "Заробітня плата за місяць")]
        [Required(ErrorMessage = "Введіть заробітню плату")]
        [Range(0, int.MaxValue, ErrorMessage = "Доступні значення з {1} до {2}")]
        public decimal Amount { get; set; }

        [Display(Name = "З віку")]
        [Required(ErrorMessage = "Введіть вік")]
        [Range(0, 100, ErrorMessage = "Доступні значення з {1} до {2}")]
        public short From { get; set; }
    }
}