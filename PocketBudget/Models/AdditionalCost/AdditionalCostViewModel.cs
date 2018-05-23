using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PocketBudget.Models.AdditionalCost
{
    public class AdditionalCostViewModel
    {
        public AdditionalCostViewModel()
        {
            Credits = new List<CreditViewModel>();
            Purchases = new List<PurchaseViewModel>();
        }

        [Display(Name = "Додаткові витрати з віку")]
        [Required(ErrorMessage = "Введіть вік початку витрат")]
        [Range(18, 80, ErrorMessage = "Доступні значення з {1} до {2}")]
        public int? From { get; set; }

        [UIHint("Checkbox")]
        public bool IsCreditAdded { get; set; }

        [UIHint("Checkbox")]
        public bool IsPurchaseAdded { get; set; }

        public IList<CreditViewModel> Credits { get; set; }

        public IList<PurchaseViewModel> Purchases { get; set; }
    }
}