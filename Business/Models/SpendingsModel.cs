using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public enum SpendingsType
    {
        Fixed,
        Percentage
    }

    public class SpendingsModel
    {
        public decimal Amount { get; set; }

        public SpendingsType Type { get; set; }
    }
}
