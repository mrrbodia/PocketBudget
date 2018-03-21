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
                decimal lastIncome = 0m;
                for (int i = additionalIncome.From; i < additionalIncome.To; ++i)
                {
                    var income = additionalIncome.GetIncomePerYear(i - additionalIncome.From);
                    if (i - additionalIncome.From < deposit.Years)
                    {
                        lastIncome = income;
                        points[i] = points[i] + income;
                    }
                    else
                    {
                        points[i] = points[i] + lastIncome;
                    }
                }
            }
        }
    }
}