using Business.DomainModel.Active;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Components.AdditionalPath
{
    public interface IAdditionalIncomeStep
    {
        void Execute(AdditionalIncome additionalIncome, List<decimal?> points);
    }
}
