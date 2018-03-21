using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models.AdditionalCost
{
    public class PurchaseViewModel : AdditionalCostItemViewModel
    {
        public bool IsActive { get; set; }
    }
}