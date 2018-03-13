using Business.DomainModel.Active;
using System;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class DepositIncomeStep : IAdditionalIncomeStep
    {
        public void Execute(AdditionalIncome additionalIncome, List<decimal?> points)
        {
            if (additionalIncome.Deposit == null)
                return;

            decimal lastIncome = 0m;
            for (int i = additionalIncome.From; i < additionalIncome.To; ++i)
            {
                var income = additionalIncome.Deposit.GetIncomePerYear() * (decimal)Math.Pow((1 + additionalIncome.Deposit.Percentage / 100), i - additionalIncome.From);
                if (i - additionalIncome.From < additionalIncome.Deposit.Years)
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
