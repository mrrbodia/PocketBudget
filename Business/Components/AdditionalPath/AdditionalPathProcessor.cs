using Business.DomainModel.Active;
using Business.DomainModel.Cost;
using Business.Models;
using System.Collections.Generic;
using System.Linq;

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
            if (path.Education != null && path.Savings.Type == SavingsType.Percentage)
            {
                if (!path.Education.IsHidden)
                {
                    var from = path.Education.EducationDegrees.First().From;
                    from = from > path.CurrentAge ? from - path.CurrentAge : 0;
                    for (int i = from; i < points.Count; ++i)
                    {
                        points[i] += points[i] * path.Education.GetIncomePercent(from, i);
                    }
                }
                additionalLines.Add(new ChartLine(
                    Constants.ChartLineType.Education,
                    points,
                    path.Savings.Amount,
                    Constants.Currency.Hrn,
                    path.Education.IsHidden));
            }
            if (path.AdditionalPath?.AdditionalIncomes != null)
            {
                foreach (var additionalIncome in path.AdditionalPath.AdditionalIncomes)
                {
                    if (additionalIncome.From < path.CurrentAge)
                        continue;

                    additionalIncome.From -= path.CurrentAge;
                    additionalIncome.To = (short)(path.LifeExpectancy - path.CurrentAge);
                    if (!additionalIncome.IsHidden)
                    {
                        foreach (var step in this.incomeSteps)
                        {
                            step.Execute(additionalIncome, points);
                        }
                    }
                    additionalIncome.From += path.CurrentAge;
                    additionalLines.Add(new ChartLine(additionalIncome.LineType, points, additionalIncome.Total, additionalIncome.CurrencyId, additionalIncome.IsHidden));
                }
            }
            if (path.AdditionalPath?.AdditionalCosts != null)
            {
                foreach (var additionalCost in path.AdditionalPath.AdditionalCosts)
                {
                    if (additionalCost.From < path.CurrentAge)
                        continue;

                    additionalCost.From -= path.CurrentAge;
                    additionalCost.To = (short)(path.LifeExpectancy - path.CurrentAge);
                    if (!additionalCost.IsHidden)
                    {
                        foreach (var step in this.costSteps)
                        {
                            step.Execute(additionalCost, points);
                        }
                    }
                    additionalCost.From += path.CurrentAge;
                    additionalLines.Add(new ChartLine(additionalCost.LineType, points, additionalCost.Total, additionalCost.CurrencyId, additionalCost.IsHidden));
                }
            }
            return additionalLines;
        }
    }
}
