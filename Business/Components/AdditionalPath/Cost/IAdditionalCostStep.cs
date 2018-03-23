using Business.DomainModel.Active;
using Business.DomainModel.Cost;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public interface IAdditionalCostStep
    {
        void Execute(IAdditionalCost additionalCost, List<decimal?> points);
    }
}
