using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Components.AdditionalPath
{
    public class DepositIncomeStep : IAdditionalIncomeStep
    {
        public void Execute(PathModel path)
        {
            if (!path.Deposit)
                return values;
            var position = PersonalFinances.AdditionalPath.FromAge - PersonalFinances.Path.CurrentAge;
            for (var i = position; i < position + PersonalFinances.AdditionalPath.Deposit.Years; i++)
            {
                values[i] = values[i] + getDepositIncomePerYear();
            }
            return values;
        }
    }
}
