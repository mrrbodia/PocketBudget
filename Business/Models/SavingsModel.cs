using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    //TODO: Percentage will affect on View (remove strategies)
    public enum SavingsType
    {
        Fixed,
        Percentage,
        Custom
    }

    public class SavingsModel
    {
        public decimal Amount { get; set; }

        public SavingsType Type { get; set; }
    }
}
