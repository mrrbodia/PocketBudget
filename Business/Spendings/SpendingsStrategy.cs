using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Spendings
{
    //TODO: add base generic strategy class for all strategies
    public abstract class SpendingsStrategy
    {
        public abstract decimal GetSpendingsLineAmount(PathModel path, decimal? savedAmount);

        public static SpendingsStrategy GetSpendingsStragery(SpendingsType type)
        {
            switch (type)
            {
                case SpendingsType.Fixed:
                    return new FixedSpendingsStrategy();
                case SpendingsType.Percentage:
                    return new PercentageSpendingsStrategy();
                default:
                    return new FixedSpendingsStrategy();
            }
        }
    }
}
