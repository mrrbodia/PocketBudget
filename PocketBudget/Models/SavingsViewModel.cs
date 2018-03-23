using Business.Models;
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
        public decimal Amount { get; set; }
        
        public SavingsType Type { get; set; }
    }
}