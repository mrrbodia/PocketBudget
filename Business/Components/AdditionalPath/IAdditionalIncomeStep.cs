using Business.DomainModel.Active;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public interface IAdditionalIncomeStep
    {
        void Execute(AdditionalIncome additionalIncome, List<decimal?> points);
    }
}
