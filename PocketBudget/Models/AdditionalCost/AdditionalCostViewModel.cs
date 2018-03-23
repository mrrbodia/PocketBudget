using System;
using System.Collections.Generic;
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

        public IEnumerable<CreditViewModel> Credits { get; set; }
    }
}