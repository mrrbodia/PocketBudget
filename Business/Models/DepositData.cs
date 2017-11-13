using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class DepositData
    {
        public string CurrencyId { get; set; }

        public decimal Amount { get; set; }

        public float Percentage { get; set; }
    }
}
