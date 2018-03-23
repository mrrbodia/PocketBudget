using AutoMapper;
using Business.DomainModel.Active;
using Business.Models;
using PocketBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketBudget.App_Start
{
    public class AdditionalIncomeResolver : IValueResolver<AdditionalPathViewModel, AdditionalPathModel, IList<IAdditionalIncome>>
    {
        public IList<IAdditionalIncome> Resolve(AdditionalPathViewModel source, AdditionalPathModel destination, IList<IAdditionalIncome> destMember, ResolutionContext context)
        {
            destMember = new List<IAdditionalIncome>();
            //TODO: IsAdded
            if (source?.AdditionalIncome?.Deposits?.Any() ?? false)
            {
                foreach (var deposit in source.AdditionalIncome.Deposits)
                {
                    destMember.Add(new Deposit()
                    {
                        CurrencyId = deposit.CurrencyId,
                        From = deposit.FromAge,
                        Percentage = deposit.Percentage,
                        Total = deposit.Total,
                        Years = deposit.Years
                    });
                }
            }
            if (source?.AdditionalIncome?.Sales?.Any() ?? false)
            {
                foreach (var sale in source.AdditionalIncome.Sales)
                {
                    destMember.Add(new Sale()
                    {
                        CurrencyId = sale.CurrencyId,
                        From = sale.FromAge,
                        Total = sale.Total
                    });
                }
            }
            return destMember;
        }
    }
}