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

        public int? From { get; set; }

        [UIHint("Checkbox")]
        public bool IsCreditAdded { get; set; }

        [UIHint("Checkbox")]
        public bool IsPurchaseAdded { get; set; }

        public IEnumerable<CreditViewModel> Credits { get; set; }

        public IEnumerable<PurchaseViewModel> Purchases { get; set; }
    }
}