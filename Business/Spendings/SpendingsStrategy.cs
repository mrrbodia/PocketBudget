using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Spendings
{
    public abstract class SpendingsStrategy : BasePathStrategy
    {
        public abstract decimal GetSpendingsLineAmount(PathModel path, decimal? savedAmount);
    }
}
