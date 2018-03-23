using System.ComponentModel.DataAnnotations;

namespace PocketBudget.Models
{
    public class SalaryViewModel
    {
        [Display(Name = "Заробітня плата / місяць")]
        [Required(ErrorMessage = "Введіть заробітню плату")]
        [Range(0, int.MaxValue, ErrorMessage = "Доступні значення з {1} до {2}")]
        public decimal Amount { get; set; }
    }
}