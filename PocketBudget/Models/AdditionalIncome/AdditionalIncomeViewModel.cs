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
            ChangedSalary = new List<ChangedSalaryViewModel>();
        }

        [Display(Name = "Додаткові доходи з віку")]
        [Required(ErrorMessage = "Введіть вік початку доходів")]
        public int? From { get; set; }
        
        [UIHint("Checkbox")]
        public bool IsDepositAdded { get; set; }

        [UIHint("Checkbox")]
        public bool IsSaleAdded { get; set; }

        [UIHint("Checkbox")]
        public bool IsSalaryChanged { get; set; }

        public IList<DepositViewModel> Deposits { get; set; }

        public IList<SaleViewModel> Sales { get; set; }

        public IList<ChangedSalaryViewModel> ChangedSalary { get; set; }
    }
}