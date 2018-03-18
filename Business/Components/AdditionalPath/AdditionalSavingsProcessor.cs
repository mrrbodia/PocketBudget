﻿using Business.Models;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class AdditionalSavingsProcessor
    {
        private readonly IEnumerable<IAdditionalIncomeStep> incomeSteps;
        private readonly IEnumerable<IAdditionalCostStep> costSteps;

        public AdditionalSavingsProcessor(IEnumerable<IAdditionalIncomeStep> incomeSteps, IEnumerable<IAdditionalCostStep> costSteps)
        {
            this.incomeSteps = incomeSteps;
            this.costSteps = costSteps;
        }

        public List<ChartLine> Execute(PathModel path, List<decimal?> points)
        {
            var additionalLines = new List<ChartLine>();
            foreach (var additionalIncome in path.AdditionalPath.AdditionalIncomes)
            {
                additionalIncome.From -= path.CurrentAge;
                additionalIncome.To = path.RetirementAge;
                foreach (var step in this.incomeSteps)
                {
                    step.Execute(additionalIncome, points);
                }
                additionalIncome.From += path.CurrentAge;
                additionalLines.Add(new ChartLine(Constants.ChartLineType.Deposit, points));
            }

            //foreach (var additionalCost in path.AdditionalPath.AdditionalCosts)
            //{
            //    additionalCost.From -= path.CurrentAge;
            //    additionalCost.To = path.RetirementAge;
            //    foreach (var step in this.costSteps)
            //    {
            //        step.Execute(additionalCost, points);
            //    }
            //    additionalCost.From += path.CurrentAge;
            //    additionalLines.Add(new ChartLine(Constants.ChartLineType.Deposit, points));
            //}
            return additionalLines;
        }
    }
}
