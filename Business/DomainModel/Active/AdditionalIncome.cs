using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DomainModel.Active
{
    public class AdditionalIncome
    {
        public int From { get; set; }

        public int To { get; set; }

        public Deposit Deposit { get; set; }
    }
}
