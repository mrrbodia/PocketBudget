using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Savings
{
    public abstract class SavingsStrategy : BasePathStrategy
    {
        public abstract decimal GetSavingsLineAmount(PathModel path, int year);
    }
}
