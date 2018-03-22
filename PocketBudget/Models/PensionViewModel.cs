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
        [Range(0, int.MaxValue, ErrorMessage = "Пенсія повинна бути більша 0")]
        public decimal Amount { get; set; }
    }
}