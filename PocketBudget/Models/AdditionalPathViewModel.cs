using System.Collections.Generic;

namespace PocketBudget.Models
{
    public class AdditionalPathViewModel
    {
        public AdditionalPathViewModel()
        {
            AdditionalIncome = new AdditionalIncomeViewModel();
        }

        public int? From { get; set; }

        public AdditionalIncomeViewModel AdditionalIncome { get; set; }

        //TODO:
        //public AdditionalCostViewModel AdditionalCost { get; set; }
    }
}