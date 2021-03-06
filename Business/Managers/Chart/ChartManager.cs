﻿using Business.Components.AdditionalPath;
using Business.Models;
using Business.Savings;
using System.Collections.Generic;
using System.Linq;

namespace Business.Managers.Chart
{
    public class ChartManager : IChartManager
    {
        private readonly AdditionalPathProcessor additionalPathProcessor;

        private SavingsStrategy savingsStrategy;

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

            if (path.AdditionalPath != null || path.Education != null)
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
                baseLine.Add(savingsStrategy.GetSavingsLineAmount(path, i));
            }
            for (int i = 1; i < workingPeriod; ++i)
            {
                baseLine[i] = baseLine[i - 1] + baseLine[i];
            }
            for (int i = workingPeriod; i < workingPeriod + retirementPeriod; ++i)
            {
                baseLine.Add(baseLine[i - 1] + (path.Pension.Amount * 12 - path.Spendings.Amount * 12));
            }
            var symbol = path.Savings.Type == SavingsType.Percentage ? Constants.Symbols.Percent : Constants.Currency.Hrn;
            return new ChartLine(Constants.ChartLineType.Base, baseLine, path.Savings.Amount, symbol);
        }

        protected void PrepareCalculationData(PathModel path)
        {
            this.savingsStrategy = BasePathStrategy.GetStragery(path.Savings.Type);
            if (path?.Salary?.SalaryPeriods?.Any() ?? false)
            {
                path.Salary.SalaryPeriods.Aggregate((f, s) => { f.To = s.From; return s; });
                path.Salary.SalaryPeriods.Last().To = path.RetirementAge;
            }
            if (path?.Education?.EducationDegrees?.Any() ?? false)
            {
                path.Education.EducationDegrees.Aggregate((f, s) => { f.To = s.From; return s; });
                path.Education.EducationDegrees.Last().To = path.LifeExpectancy;
            }
        }

        protected List<ChartLine> AddAdditionalLines(PathModel path, List<decimal?> mainSavingsLine)
        {
            var additionalLine = new List<decimal?>(mainSavingsLine);
            return additionalPathProcessor.Execute(path, additionalLine);
        }
    }
}