using Business.Models;
using System.Collections.Generic;

namespace Business.Components.AdditionalPath
{
    public class AdditionalSavingsProcessor
    {
        private readonly IEnumerable<IAdditionalIncomeStep> steps;

        public AdditionalSavingsProcessor(IEnumerable<IAdditionalIncomeStep> steps)
        {
            this.steps = steps;
        }

        public void Execute(PathModel path, List<decimal?> points)
        {
            foreach (var additionalIncome in path.AdditionalPath.AdditionalIncomes)
            {
                foreach (var step in this.steps)
                {
                    step.Execute(additionalIncome, points);
                }
            }
        }
    }
}
