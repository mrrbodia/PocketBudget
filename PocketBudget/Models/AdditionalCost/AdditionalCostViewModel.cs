using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class AdditionalCostViewModel
    {
        public AdditionalCostViewModel()
        {
            Credits = new List<CreditViewModel>();
        }

        public int? From { get; set; }

        [UIHint("Checkbox")]
        public bool IsCreditAdded { get; set; }

        public IEnumerable<CreditViewModel> Credits { get; set; }
    }
}