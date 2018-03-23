using Business.DomainModel.Active;
using Business.DomainModel.Cost;
using System;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class CreditCostStep : IAdditionalCostStep
    {
        public void Execute(IAdditionalCost additionalCost, List<decimal?> points)
        {
            if (additionalCost is Credit credit)
            {
            }
        }
    }
}