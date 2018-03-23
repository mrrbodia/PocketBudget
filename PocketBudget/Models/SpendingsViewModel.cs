using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace PocketBudget.Models
{
    public class SpendingsViewModel
    {
        [Display(Name = "Витрати на пенсії / місяць")]
        [Required(ErrorMessage = "Введіть витрати на пенсії")]
        [Range(0, int.MaxValue, ErrorMessage = "Доступні значення з {1} до {2}")]
        public decimal Amount { get; set; }
        
        public SpendingsType Type { get; set; }
    }
}