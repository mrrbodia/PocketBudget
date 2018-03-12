using Business.Models;
using Business.Savings;
using Business.Spendings;

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
                case SavingsType.Custom:
                    return new CustomSavingsStrategy();
                default:
                    return new FixedSavingsStrategy();
            }
        }

        public static SpendingsStrategy GetStragery(SpendingsType type)
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
