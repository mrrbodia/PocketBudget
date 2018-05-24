using Business;
using PocketBudget.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBudget.Models
{
    public abstract class AdditionalCostItemViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Доступні значення з {1} до {2}")]
        public virtual decimal Total { get; set; }

        public virtual string CurrencyId { get; set; }

        public virtual short FromAge { get; set; }

        public virtual bool IsActive { get; set; }

        public abstract string Title { get; }

        public virtual bool IsHidden { get; set; }

        public virtual string CurrencySymbol(string currencyId)
        {
            return ViewHelper.GetCurrencySymbol(currencyId);
        }
    }
}
