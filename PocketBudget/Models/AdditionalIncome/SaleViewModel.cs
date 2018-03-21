using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class SaleViewModel : AdditionalIncomeItemViewModel
    {
        public bool IsActive { get; set; }
    }
}