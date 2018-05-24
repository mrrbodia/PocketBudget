using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PocketBudget.Models
{
    public class AdditionalIncomeViewModel
    {
        public AdditionalIncomeViewModel()
        {
            Deposits = new List<DepositViewModel>();
            Sales = new List<SaleViewModel>();
        }

        [Display(Name = "Додаткові доходи з віку")]
        [Required(ErrorMessage = "Введіть вік початку доходів")]
        [Range(18, 80, ErrorMessage = "Доступні значення з {1} до {2}")]
        public int? From { get; set; }
        
        [UIHint("Checkbox")]
        public bool IsDepositAdded { get; set; }

        [UIHint("Checkbox")]
        public bool IsSaleAdded { get; set; }

        public IList<DepositViewModel> Deposits { get; set; }

        public IList<SaleViewModel> Sales { get; set; }
    }
}