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
    public class AdditionalIncomeViewResolver : IValueResolver<AdditionalPathModel, AdditionalPathViewModel, AdditionalIncomeViewModel>
    {
        public AdditionalIncomeViewModel Resolve(AdditionalPathModel source, AdditionalPathViewModel destination, AdditionalIncomeViewModel destMember, ResolutionContext context)
        {
            destMember = new AdditionalIncomeViewModel();
            destMember.Deposits = new List<DepositViewModel>();
            destMember.Sales = new List<SaleViewModel>();
            if (source?.AdditionalIncomes?.Any() ?? false)
            {
                foreach (var income in source.AdditionalIncomes)
                {
                    if (income is Deposit deposit)
                    {
                        destMember.Deposits.Add(new DepositViewModel()
                        {
                            CurrencyId = deposit.CurrencyId,
                            FromAge = deposit.From,
                            IsActive = deposit.IsActive,
                            IsHidden = deposit.IsHidden,
                            Percentage = (float)deposit.Percentage,
                            Total = deposit.Total,
                            Years = deposit.Years
                        });
                    }
                    else if (income is Sale sale)
                    {
                        destMember.Sales.Add(new SaleViewModel()
                        {
                            CurrencyId = sale.CurrencyId,
                            IsActive = sale.IsActive,
                            FromAge = sale.From,
                            IsHidden = sale.IsHidden,
                            Total = sale.Total,
                        });
                    }
                }
            }
            return destMember;
        }
    }
}