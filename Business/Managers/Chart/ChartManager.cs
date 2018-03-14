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
        private readonly AdditionalSavingsProcessor additionalSavingsProcessor;

        private SavingsStrategy savingsStrategy;

        private SpendingsStrategy spendingsStrategy;

        public ChartManager(AdditionalSavingsProcessor additionalSavingsProcessor)
        {
            this.additionalSavingsProcessor = additionalSavingsProcessor;
        }

        public List<ChartLine> GetChartLines(PathModel path)
        {
            var chartLines = new List<ChartLine>();
            PrepareCalculationData(path);
            var savingsLines = GetSavingsLines(path);
            var spendingLines = GetSpendingLines(path, savingsLines);

            chartLines.AddRange(savingsLines);
            chartLines.AddRange(spendingLines);
            return chartLines;
        }

        protected void PrepareCalculationData(PathModel path)
        {
            this.savingsStrategy = BasePathStrategy.GetStragery(path.Savings.Type);
            this.spendingsStrategy = BasePathStrategy.GetStragery(path.Spendings.Type);
        }

        protected List<ChartLine> GetSavingsLines(PathModel path)
        {
            var savingsLines = new List<ChartLine>();
            savingsLines.Add(GetSavingsLine(path));
            if (path.AdditionalPath == null || !path.AdditionalPath.AdditionalIncomes.Any())
                return savingsLines;

            foreach (var additionalIncome in path.AdditionalPath?.AdditionalIncomes)
            {
                savingsLines.Add(GetAdditionalSavingsLine(path, savingsLines.First().Points));
            }
            return savingsLines;
        }

        protected ChartLine GetSavingsLine(PathModel path)
        {
            var savingsLine = new List<decimal?>();
            var workingPeriod = path.RetirementAge - path.CurrentAge;
            for (int i = 0; i < workingPeriod; ++i)
            {
                savingsLine.Add(savingsStrategy.GetSavingsLineAmount(path));
            }
            for (int i = 1; i < workingPeriod; ++i)
            {
                savingsLine[i] = savingsLine[i - 1] + savingsLine[i];
            }
            for (int i = workingPeriod; i < path.RetirementAge; ++i)
            {
                savingsLine.Add(null);
            }
            return new ChartLine(Constants.ChartLineType.Savings, savingsLine);
        }

        protected ChartLine GetAdditionalSavingsLine(PathModel path, List<decimal?> mainSavingsLine)
        {
            //TODO: put ParentLine into parameter
            var additionalLine = new List<decimal?>(mainSavingsLine);
            additionalSavingsProcessor.Execute(path, additionalLine);
            return new ChartLine(Constants.ChartLineType.Savings, additionalLine);
        }

        protected List<ChartLine> GetSpendingLines(PathModel path, List<ChartLine> savingsLines)
        {
            var spendingLines = new List<ChartLine>();
            foreach (var line in savingsLines)
            {
                spendingLines.Add(GetSpendingLine(path, line.Points));
            }
            return spendingLines;
        }

        protected ChartLine GetSpendingLine(PathModel path, List<decimal?> savingsLines)
        {
            var spendingLine = new List<decimal?>();
            var workingPeriod = path.RetirementAge - path.CurrentAge;
            var retirementPeriod = path.LifeExpectancy - path.RetirementAge;
            for (int i = 0; i < workingPeriod; ++i)
                spendingLine.Add(null);
            spendingLine[workingPeriod - 1] = savingsLines[workingPeriod - 1];
            for (int i = workingPeriod; i < workingPeriod + retirementPeriod; ++i)
                spendingLine.Add(spendingLine[i-1] + spendingsStrategy.GetSpendingsLineAmount(path, savingsLines[workingPeriod - 1]));
            //additionalSpendingsProcessor.Execute();
            return new ChartLine(Constants.ChartLineType.Spendings, spendingLine);
        }
    }
}