using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public class Deposit : IActive
    {
        public virtual float Percentage { get; set; }

        public virtual decimal Total { get; set; }

        public virtual string CurrencyId { get; set; }

        public virtual short Years { get; set; }

        public virtual decimal GetIncomePerYear()
        {
            return 0;
        }
    }
}