using Business.Models;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    //TODO: Refactor
    public class AdditionalPathProcessor
    {
        private readonly IEnumerable<IAdditionalIncomeStep> incomeSteps;
        private readonly IEnumerable<IAdditionalCostStep> costSteps;

        public AdditionalPathProcessor(IEnumerable<IAdditionalIncomeStep> incomeSteps, IEnumerable<IAdditionalCostStep> costSteps)
        {
            this.incomeSteps = incomeSteps;
            this.costSteps = costSteps;
        }

        public List<ChartLine> Execute(PathModel path, List<decimal?> points)
        {
            var additionalLines = new List<ChartLine>();

            foreach (var additionalIncome in path.AdditionalPath.AdditionalIncomes)
            {
                if (additionalIncome.From < path.CurrentAge)
                    continue;

                additionalIncome.From -= path.CurrentAge;
                additionalIncome.To = (short)(path.LifeExpectancy - path.CurrentAge);
                foreach (var step in this.incomeSteps)
                {
                    step.Execute(additionalIncome, points);
                }
                additionalIncome.From += path.CurrentAge;
                additionalLines.Add(new ChartLine(Constants.ChartLineType.Deposit, points, additionalIncome.IsHidden));
            }
            foreach (var additionalCost in path.AdditionalPath.AdditionalCosts)
            {
                if (additionalCost.From < path.CurrentAge)
                    continue;

                additionalCost.From -= path.CurrentAge;
                additionalCost.To = (short)(path.LifeExpectancy - path.CurrentAge);
                foreach (var step in this.costSteps)
                {
                    step.Execute(additionalCost, points);
                }
                additionalCost.From += path.CurrentAge;
                additionalLines.Add(new ChartLine(Constants.ChartLineType.Credit, points));
            }
            return additionalLines;
        }
    }
}
