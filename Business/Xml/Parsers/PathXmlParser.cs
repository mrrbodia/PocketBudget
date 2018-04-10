using Business.DomainModel.Active;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.Xml.Parsers
{
    public class PathXmlParser
    {
        public PathModel ParsePath(XElement element)
        {
            var model = new PathModel();
            model.CurrentAge = short.Parse(element.Element("CurrentAge").Value);
            model.RetirementAge = short.Parse(element.Element("RetirementAge").Value);
            model.LifeExpectancy = short.Parse(element.Element("LifeExpectancy").Value);
            model.Salary = ParseSalary(element.Element("Salary"));
            model.Savings = ParseSavings(element.Element("SavingsModel"));
            model.Spendings = ParseSpendings(element.Element("SpendingsModel"));
            model.Pension = ParsePension(element.Element("PensionModel"));
            model.AdditionalPath = ParseAdditionalPath(element.Element("AdditionalPath"));
            return model;
        }

        private SalaryModel ParseSalary(XElement element)
        {
            var salary = new SalaryModel();
            salary.SalaryPeriods = element.Element("SalaryPeriods").Elements("SalaryPeriod").Select(p => ParseSalaryPeriod(p)).ToList();
            return salary;
        }

        private SpendingsModel ParseSpendings(XElement element)
        {
            var spendings = new SpendingsModel();
            spendings.Amount = decimal.Parse(element.Element("Amount").Value);
            return spendings;
        }

        private PensionModel ParsePension(XElement element)
        {
            var pension = new PensionModel();
            pension.Amount = decimal.Parse(element.Element("Amount").Value);
            return pension;
        }

        private SavingsModel ParseSavings(XElement element)
        {
            var savings = new SavingsModel();
            savings.Amount = decimal.Parse(element.Element("Amount").Value);
            savings.Type = (SavingsType)Enum.Parse(typeof(SavingsType), element.Element("Type").Value);
            return savings;
        }

        private SalaryPeriod ParseSalaryPeriod(XElement element)
        {
            var period = new SalaryPeriod();
            period.From = short.Parse(element.Element("From").Value);
            period.Amount = short.Parse(element.Element("Amount").Value);
            return period;
        }

        private AdditionalPathModel ParseAdditionalPath(XElement element)
        {
            var additionalPath = new AdditionalPathModel();
            if(element != null)
            {
                additionalPath.AdditionalIncomes = element
                    .Element("AdditionalIncomes")
                    .Elements("AdditionalIncome")
                    .Select(i => ParseAdditionalIncomes(i))
                    .ToList();

                additionalPath.AdditionalCosts = element
                    .Element("AdditionalCosts")
                    .Elements("AdditionalCost")
                    .Select(c => c)
                    .ToList();
            }
            return new AdditionalPathModel();
        }

        private IAdditionalIncome ParseAdditionalIncomes(XElement element)
        {
            var type = element.Element("LineType").Value;
            if (string.IsNullOrEmpty(type))
                return null;

            if(type.Equals(Constants.ChartLineType.Deposit))
            {
                var deposit = new Deposit();
                deposit.From = short.Parse(element.Element("From").Value);
                deposit.IsActive = bool.Parse(element.Element("IsActive").Value);
                deposit.IsHidden = bool.Parse(element.Element("IsHidden").Value);
                deposit.Percentage = double.Parse(element.Element("Percentage").Value);
                deposit.Total = decimal.Parse(element.Element("Total").Value);
                deposit.Years = short.Parse(element.Element("Years").Value);
            }

            return additionalIncome;
        }
    }
}
