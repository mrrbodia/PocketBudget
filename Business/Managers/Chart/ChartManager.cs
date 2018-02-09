using Business.Components.AdditionalPath;
using Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace Business.Managers.Chart
{
    public class ChartManager : IChartManager
    {
        private readonly AdditionalSavingsProcessor additionalSavingsProcessor;

        public ChartManager(AdditionalSavingsProcessor additionalSavingsProcessor)
        {
            this.additionalSavingsProcessor = additionalSavingsProcessor;
        }

        public List<ChartLine> GetChartLines(PathModel path)
        {
            var chartLines = new List<ChartLine>();
            var savingsLines = GetSavingsLines(path);
            var spendingLines = GetSpendingLines(path, savingsLines);

            chartLines.AddRange(savingsLines);
            chartLines.AddRange(spendingLines);
            return chartLines;
        }
        
        protected List<ChartLine> GetSavingsLines(PathModel path)
        {
            var savingsLines = new List<ChartLine>();
            savingsLines.Add(GetSavingsLine(path));
            if (path.AdditionalPath == null || !path.AdditionalPath.AdditionalIncomes.Any())
                return savingsLines;

            foreach (var additionalIncome in path.AdditionalPath?.AdditionalIncomes)
            {
                //TODO: put ParentLine into parameter
                var additionalLine = new List<decimal?>(savingsLines.First().Points);
                //TODO: will be removed
                additionalIncome.From = additionalIncome.Deposit.FromAge - path.CurrentAge;
                additionalIncome.To = path.RetirementAge;
                additionalSavingsProcessor.Execute(path, additionalLine);
                savingsLines.Add(new ChartLine(Constants.ChartLineType.Savings, additionalLine));
            }
            return savingsLines;
        }

        protected ChartLine GetSavingsLine(PathModel path)
        {
            var savingsLine = new List<decimal?>();
            var workingPeriod = path.RetirementAge - path.CurrentAge;
            for (int i = 0; i < workingPeriod; ++i)
            {
                savingsLine.Add(path.Savings.Amount * 12);
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
                spendingLine.Add(spendingLine[i - 1] - path.Spendings.Amount * 12);
            //additionalSpendingsProcessor.Execute();
            return new ChartLine(Constants.ChartLineType.Spendings, spendingLine);
        }
    }
}