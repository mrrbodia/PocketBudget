using Business.DomainModel.Cost;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class PurchaseCostStep : IAdditionalCostStep
    {
        public void Execute(IAdditionalCost additionalCost, List<decimal?> points)
        {
            if (additionalCost is Purchase purchase)
            {
                for (int i = additionalCost.From; i < additionalCost.To; ++i)
                {
                    var cost = purchase.GetCostPerYear();
                    points[i] = points[i] + cost;
                }
            }
        }
    }
}