using Business.DomainModel.Active;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class DepositIncomeStep : IAdditionalIncomeStep
    {
        public void Execute(AdditionalIncome additionalIncome, List<decimal?> points)
        {
            if (additionalIncome.Deposit == null)
                return;

            for (int i = additionalIncome.From; i < additionalIncome.To; ++i)
            {
                //var income = additionalIncome.Deposit.Total * (decimal)Math.Pow((1 + additionalIncome.Deposit.Percentage / 100), i - additionalIncome.From);
                points[i] = points[i] + additionalIncome.Deposit.GetIncomePerYear();
            }
        }
    }
}
