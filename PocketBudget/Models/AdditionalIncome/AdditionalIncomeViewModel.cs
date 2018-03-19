using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PocketBudget.Models
{
    public class AdditionalIncomeViewModel
    {
        public AdditionalIncomeViewModel()
        {
            Deposits = new List<DepositViewModel>();
        }

        public int? From { get; set; }
        
        [UIHint("Checkbox")]
        public bool IsDepositAdded { get; set; }

        public IEnumerable<DepositViewModel> Deposits { get; set; }
    }
}