using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace PocketBudget.Models
{
    public class SpendingsViewModel
    {
        [Display(Name = "Витрати на пенсії / місяць")]
        public decimal Amount { get; set; }
        
        public SpendingsType Type { get; set; }
    }
}