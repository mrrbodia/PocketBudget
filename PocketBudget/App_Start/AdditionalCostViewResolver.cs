using AutoMapper;
using Business.DomainModel.Active;
using Business.DomainModel.Cost;
using Business.Models;
using PocketBudget.Models;
using PocketBudget.Models.AdditionalCost;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketBudget.App_Start
{
    public class AdditionalCostViewResolver : IValueResolver<AdditionalPathModel, AdditionalPathViewModel, AdditionalCostViewModel>
    {
        public AdditionalCostViewModel Resolve(AdditionalPathModel source, AdditionalPathViewModel destination, AdditionalCostViewModel destMember, ResolutionContext context)
        {
            destMember = new AdditionalCostViewModel();
            destMember.Purchases = new List<PurchaseViewModel>();
            destMember.Credits = new List<CreditViewModel>();
            if (source?.AdditionalCosts?.Any() ?? false)
            {
                foreach (var cost in source.AdditionalCosts)
                {
                    if (cost is Credit credit)
                    {
                        destMember.Credits.Add(new CreditViewModel()
                        {
                            CurrencyId = credit.CurrencyId,
                            FromAge = credit.From,
                            IsHidden = credit.IsHidden,
                            Percentage = (float)credit.Percentage,
                            Total = credit.Total,
                            Years = credit.Years,
                            IsActive = true
                        });
                    }
                    else if (cost is Purchase purchase)
                    {
                        destMember.Purchases.Add(new PurchaseViewModel()
                        {
                            CurrencyId = purchase.CurrencyId,
                            FromAge = purchase.From,
                            IsHidden = purchase.IsHidden,
                            Total = purchase.Total,
                            IsActive = true
                        });
                    }
                }
            }
            return destMember;
        }
    }
}