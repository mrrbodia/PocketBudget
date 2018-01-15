using Business.Components.AdditionalPath;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Chart
{
    public class ChartManager : IChartManager
    {
        private readonly AdditionalSavingsProcessor additionalSavingsProcessor;

        public ChartManager(AdditionalSavingsProcessor additionalSavingsProcessor)
        {
            this.additionalSavingsProcessor = additionalSavingsProcessor;
        }

        public List<List<decimal?>> GetChartLines(PathModel path)
        {
            var result = new List<List<decimal?>>();
            var savingsLines = GetSavingsLines(path);
            result.AddRange(savingsLines);
            result.AddRange(GetSpendingLines(path, savingsLines));
            return result;
        }

        protected List<List<decimal?>> GetSavingsLines(PathModel path)
        {
            var savingsLines = new List<List<decimal?>>();
            savingsLines.Add(GetSavingsLine(path));
            //foreach (var fork in path.Lines)
            //{
                //savingsLines.Add(GetSavingsLine(path, fork));
            //}
            return savingsLines;
        }

        protected List<decimal?> GetSavingsLine(PathModel path)
        {
            var savingsLine = new List<decimal?>();
            var workingPeriod = path.RetirementAge - path.CurrentAge;
            for (int i = 0; i < workingPeriod; ++i)
            {
                savingsLine.Add(path.Savings * 12);
            }
            //additionalSavingsProcessor.Execute();
            for (int i = 1; i < workingPeriod; ++i)
            {
                savingsLine[i] = savingsLine[i - 1] + savingsLine[i];
            }
            return savingsLine;
        }

        protected List<List<decimal?>> GetSpendingLines(PathModel path, List<List<decimal?>> savingsLines)
        {
            var spendingLines = new List<List<decimal?>>();
            //foreach (var line in savingsLines)
            spendingLines.Add(GetSpendingLine(path, savingsLines[0]));
            return spendingLines;
        }

        protected List<decimal?> GetSpendingLine(PathModel path, List<decimal?> savingsLines)
        {
            var spendingLines = new List<decimal?>();
            var workingPeriod = path.RetirementAge - path.CurrentAge;
            var retirementPeriod = path.LifeExpectancy - path.RetirementAge;
            for (int i = 0; i < workingPeriod; ++i)
                spendingLines.Add(null);
            spendingLines[workingPeriod - 1] = savingsLines[workingPeriod - 1];
            for (int i = workingPeriod; i < workingPeriod + retirementPeriod; ++i)
                spendingLines.Add(spendingLines[i - 1] - path.Spendings * 12);
            //additionalSpendingsProcessor.Execute();
            return spendingLines;
        }
    }
}