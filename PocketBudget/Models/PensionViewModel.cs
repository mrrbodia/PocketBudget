using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class PensionViewModel
    {
        [Display(Name = "Пенсія")]
        [Required(ErrorMessage = "Введіть розмір пенсії")]
        [Range(0, 20000, ErrorMessage = "Доступні значення з {1} до {2}")]
        public decimal Amount { get; set; }
    }
}