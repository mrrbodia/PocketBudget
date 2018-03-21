using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBudget.Models
{
    public abstract class AdditionalIncomeItemViewModel
    {
        public virtual decimal Total { get; set; }

        public virtual string CurrencyId { get; set; }

        public virtual short FromAge { get; set; }
    }
}
