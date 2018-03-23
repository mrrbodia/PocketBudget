using AutoMapper;
using Business.DomainModel.Active;
using Business.DomainModel.Cost;
using Business.Models;
using PocketBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketBudget.App_Start
{
    public class AdditionalCostResolver : IValueResolver<AdditionalPathViewModel, AdditionalPathModel, IList<IAdditionalCost>>
    {
        public IList<IAdditionalCost> Resolve(AdditionalPathViewModel source, AdditionalPathModel destination, IList<IAdditionalCost> destMember, ResolutionContext context)
        {
            destMember = new List<IAdditionalCost>();
            //TODO: IsAdded
            if (source?.AdditionalCost?.Credits?.Any() ?? false)
            {
                foreach (var credit in source.AdditionalCost.Credits)
                {
                    destMember.Add(new Credit()
                    {
                        CurrencyId = credit.CurrencyId,
                        From = credit.FromAge,
                        Percentage = credit.Percentage,
                        Total = credit.Total,
                        Years = credit.Years
                    });
                }
            }
            if (source?.AdditionalCost?.Purchases?.Any() ?? false)
            {
                foreach (var credit in source.AdditionalCost.Purchases)
                {
                    destMember.Add(new Purchase()
                    {
                        CurrencyId = credit.CurrencyId,
                        From = credit.FromAge,
                        Total = credit.Total
                    });
                }
            }
            return destMember;
        }
    }
}