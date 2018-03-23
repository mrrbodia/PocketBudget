using AutoMapper;
using Business.DomainModel.Active;
using Business.Models;
using PocketBudget.Models;
using System;
using System.Collections.Generic;

namespace PocketBudget.App_Start
{
    public class AdditionalIncomeResolver : IValueResolver<AdditionalPathViewModel, AdditionalPathModel, IList<IAdditionalIncome>>
    {
        public IList<IAdditionalIncome> Resolve(AdditionalPathViewModel source, AdditionalPathModel destination, IList<IAdditionalIncome> destMember, ResolutionContext context)
        {
            destMember = new List<IAdditionalIncome>();
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
            return destMember;
        }
    }
}