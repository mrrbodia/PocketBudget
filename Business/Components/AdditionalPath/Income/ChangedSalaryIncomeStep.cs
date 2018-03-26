using Business.DomainModel.Active;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class ChangedSalaryIncomeStep : IAdditionalIncomeStep
    {
        public void Execute(IAdditionalIncome additionalIncome, List<decimal?> points)
        {
            if (additionalIncome is ChangedSalary changedSalary)
            {
                for (int i = additionalIncome.From; i < additionalIncome.To; ++i)
                {
                    var income = additionalIncome.GetIncomePerYear(i - additionalIncome.From);
                    points[i] = points[i] + income;
                }
            }
        }
    }
}