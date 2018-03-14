using Business.DomainModel.Active;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class DepositIncomeStep : IAdditionalIncomeStep
    {
        public void Execute(IAdditionalIncome additionalIncome, List<decimal?> points)
        {
            if (additionalIncome is Deposit deposit)
            {
                decimal difference = 0;
                for (int i = deposit.From; i < deposit.From + deposit.Years; ++i)
                {
                    //TODO: for difficult percents
                    if (i == deposit.From + deposit.Years - 1)
                        difference = deposit.GetIncomePerYear();
                    points[i] = points[i] + deposit.GetIncomePerYear();
                }
                for (int i = deposit.From + deposit.Years; i < deposit.To; ++i)
                {
                    points[i] += difference;
                }
            }
        }
    }
}
