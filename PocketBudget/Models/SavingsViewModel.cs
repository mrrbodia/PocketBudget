﻿using Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class SavingsViewModel
    {
        [Display(Name = "Збереження / місяць")]
        [Required(ErrorMessage = "Введіть збереження за місяць")]
        [Range(0, int.MaxValue, ErrorMessage = "Збереження повинні бути більші 0")]
        public decimal Amount { get; set; }
        
        public SavingsType Type { get; set; }
    }
}