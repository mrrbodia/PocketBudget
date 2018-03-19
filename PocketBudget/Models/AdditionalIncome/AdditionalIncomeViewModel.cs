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

        public int? From { get; set; }
        
        [UIHint("Checkbox")]
        public bool IsDepositAdded { get; set; }

        [UIHint("Checkbox")]
        public bool IsSaleAdded { get; set; }

        public IEnumerable<DepositViewModel> Deposits { get; set; }

        public IEnumerable<SaleViewModel> Sales { get; set; }
    }
}