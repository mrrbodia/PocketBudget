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
        private readonly AdditionalPathProcessor additionalPathProcessor;

        public ChartManager(AdditionalPathProcessor additionalPathProcessor)
        {
            this.additionalPathProcessor = additionalPathProcessor;
        }

        public List<List<decimal>> GetChartLines(PathModel path)
        {
            var result = new List<List<decimal>>();
            result.AddRange(GetSavingsLines(path));
            //result.Add();
            return result;
        }

        protected List<List<decimal>> GetSavingsLines(PathModel path)
        {
            //var values = [];
            //for (var i = 0; i < PersonalFinances.Path.AgeRetirement - PersonalFinances.Path.CurrentAge; i++) {
            //    values[i] = PersonalFinances.Path.Savings * 12;
            //}
            ////TODO:check a possibility to add additional values in retirement age
            //values = getAdditionalSavingsValues(values);
            //for (i = 1; i < PersonalFinances.Path.AgeRetirement - PersonalFinances.Path.CurrentAge; i++) {
            //    values[i] = values[i - 1] + values[i];
            //}
            //return values;
            var savingsLines = new List<List<decimal>>();
            //foreach (var fork in path.Lines)
            //{
                //savingsLines.Add(GetSavingsLine(path, fork));
            //}
            return savingsLines;
        }

        protected List<decimal> GetSavingsLine(PathModel path, Fork fork)
        {
            var savingsLine = new List<decimal>();
            for (int i = 0; i < path.RetirementAge; ++i)
            {
                savingsLine.Add(fork.Savings * 12);
            }
            //additionalPathProcessor.Execute();
            return savingsLine;
        }
    }
}