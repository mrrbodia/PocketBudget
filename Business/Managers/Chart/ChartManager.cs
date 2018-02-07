using Business.Components.AdditionalPath;
using Business.DomainModel.Active;
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
            var baseSavingsLine = GetSavingsLine(path);
            var spendingLines = GetSpendingLines(path, savingsLines);

            var depositLines = GetSavingsByDepositsLines(path, baseSavingsLine);
            result.AddRange(savingsLines);
            result.AddRange(GetSpendingLines(path, savingsLines));
            result.AddRange(depositLines);
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

        protected List<List<decimal?>> GetSavingsByDepositsLines(PathModel path, List<decimal?> savingsWithoutAdditionalParameters)
        {
            var savingsLines = new List<List<decimal?>>();
            if(path.AdditionalPath != null)
            {
                foreach (var deposit in path.AdditionalPath.Deposits)
                {
                    var line = GetSavingsByDepositsLines(path, savingsWithoutAdditionalParameters, deposit);
                    savingsLines.Add(line);
                }
            }
            return savingsLines;
        }

        protected List<decimal?> GetSavingsByDepositsLines(PathModel path, List<decimal?> savingsWithoutAdditionalParameters, Deposit deposit)
        {
            var savingsLine = new List<decimal?>();
            var startPoint = deposit.FromAge - path.CurrentAge;
            for (int i = 0 ; i < startPoint - 1; ++i)
            {
                savingsLine.Add(null);
            }

            savingsLine.Add(savingsWithoutAdditionalParameters[startPoint - 1]);
            var difference = 0m;
            for (int i = startPoint; i < startPoint + deposit.Years; ++i)
            {
                var incomeMoney = (double)deposit.Total * Math.Pow((1 + (double)deposit.Percentage / 100), i - startPoint);
                difference = (decimal)incomeMoney;
                savingsLine.Add(savingsWithoutAdditionalParameters[i] + difference);
            }

            for(int i = startPoint + deposit.Years; i < path.RetirementAge; ++i)
            {
                savingsLine.Add(savingsWithoutAdditionalParameters[i] + difference);
            }
            return savingsLine;
        }

        protected List<decimal?> GetSavingsLine(PathModel path)
        {
            var savingsLine = new List<decimal?>();
            var workingPeriod = path.RetirementAge - path.CurrentAge;
            for (int i = 0; i < workingPeriod; ++i)
            {
                savingsLine.Add(path.Savings * 12);
            }
            for (int i = 1; i < workingPeriod; ++i)
            {
                savingsLine[i] = savingsLine[i - 1] + savingsLine[i];
            }

            for (int i = workingPeriod; i < path.RetirementAge; ++i)
            {
                savingsLine.Add(null);
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