using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Deposit : IActive
    {
        public float Percentage { get; set; }

        public decimal Total { get; set; }

        public string CurrencyId { get; set; }

        public short Years { get; set; }

        public decimal GetIncomePerYear()
        {
            return 0;
        }
    }
}
