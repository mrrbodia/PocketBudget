using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Savings
{
    public abstract class SavingsStrategy
    {
        public abstract decimal GetSavingsLineAmount(PathModel path);

        public static SavingsStrategy GetSavingsStragery(SavingsType type)
        {
            switch (type)
            {
                case SavingsType.Fixed:
                    return new FixedSavingsStrategy();
                case SavingsType.Percentage:
                    return new PercentageSavingsStrategy();
                case SavingsType.Custom:
                    return new CustomSavingsStrategy();
                default:
                    return new FixedSavingsStrategy();
            }
        }
    }
}
