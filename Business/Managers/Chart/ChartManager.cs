using Business.Components.AdditionalPath;
using Business.Models;
using Business.Savings;
using Business.Spendings;
using System.Collections.Generic;
using System.Linq;

namespace Business.Managers.Chart
{
    public class ChartManager : IChartManager
    {
        private readonly AdditionalPathProcessor additionalPathProcessor;

        private SavingsStrategy savingsStrategy;

        private SpendingsStrategy spendingsStrategy;

        public ChartManager(AdditionalPathProcessor additionalPathProcessor)
        {
            this.additionalPathProcessor = additionalPathProcessor;
        }

        public List<ChartLine> GetChartLines(PathModel path)
        {
            var chartLines = new List<ChartLine>();
            PrepareCalculationData(path);
            var baseLine = GetPathBaseLine(path);
            chartLines.Add(baseLine);

            if (path.AdditionalPath != null)
            {
                chartLines.AddRange(AddAdditionalLines(path, baseLine.Points));
            }

            return chartLines;
        }

        public ChartLine GetPathBaseLine(PathModel path)
        {
            var baseLine = new List<decimal?>();
            var workingPeriod = path.RetirementAge - path.CurrentAge;
            var retirementPeriod = path.LifeExpectancy - path.RetirementAge;

            for (int i = 0; i < workingPeriod; ++i)
            {
                baseLine.Add(savingsStrategy.GetSavingsLineAmount(path));
            }
            for (int i = 1; i < workingPeriod; ++i)
            {
                baseLine[i] = baseLine[i - 1] + baseLine[i];
            }
            for (int i = workingPeriod; i < workingPeriod + retirementPeriod; ++i)
            {
                baseLine.Add(baseLine[i - 1] + spendingsStrategy.GetSpendingsLineAmount(path, baseLine[workingPeriod - 1]));
            }
            return new ChartLine(Constants.ChartLineType.Base, baseLine);
        }

        protected void PrepareCalculationData(PathModel path)
        {
            this.savingsStrategy = BasePathStrategy.GetStragery(path.Savings.Type);
            this.spendingsStrategy = BasePathStrategy.GetStragery(path.Spendings.Type);
        }

        protected List<ChartLine> AddAdditionalLines(PathModel path, List<decimal?> mainSavingsLine)
        {
            //TODO: put ParentLine into parameter
            var additionalLine = new List<decimal?>(mainSavingsLine);
            return additionalPathProcessor.Execute(path, additionalLine);
        }
    }
}