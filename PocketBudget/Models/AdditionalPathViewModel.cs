using System.Collections.Generic;

namespace PocketBudget.Models
{
    public class AdditionalPathViewModel
    {
        public AdditionalPathViewModel()
        {
            AdditionalIncome = new AdditionalIncomeViewModel();
        }
        
        public AdditionalIncomeViewModel AdditionalIncome { get; set; }

        public AdditionalCostViewModel AdditionalCost { get; set; }
    }
}