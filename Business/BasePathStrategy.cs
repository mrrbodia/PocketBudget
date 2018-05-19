using Business.Models;
using Business.Savings;

namespace Business
{
    public abstract class BasePathStrategy
    {
        public static SavingsStrategy GetStragery(SavingsType type)
        {
            switch (type)
            {
                case SavingsType.Fixed:
                    return new FixedSavingsStrategy();
                case SavingsType.Percentage:
                    return new PercentageSavingsStrategy();
                default:
                    return new FixedSavingsStrategy();
            }
        }
    }
}
