using System.Collections.Generic;

namespace PocketBudget.Models
{
    public class AdditionalPathViewModel
    {
        public AdditionalPathViewModel()
        {
            AdditionalIncome = new AdditionalIncomeViewModel();
        }

        //TODO: move checkboxes etc to Form element
        public AdditionalIncomeViewModel AdditionalIncome { get; set; }

        public AdditionalCostViewModel AdditionalCost { get; set; }
    }
}