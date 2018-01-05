using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Fork
    {
        public short FromAge { get; set; }

        public decimal Savings { get; set; }

        public decimal Spendings { get; set; }

        public string ForkReason { get; set; }
    }
}
