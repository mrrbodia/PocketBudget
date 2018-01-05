using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Components.AdditionalPath
{
    public class AdditionalPathProcessor
    {
        private readonly IEnumerable<IAdditionalIncomeStep> steps;

        public AdditionalPathProcessor(IEnumerable<IAdditionalIncomeStep> steps)
        {
            this.steps = steps;
        }

        public void Execute()
        {
            foreach (var step in this.steps)
            {
                step.Execute();
            }
        }
    }
}
