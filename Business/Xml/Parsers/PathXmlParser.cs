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
                additionalPath.AdditionalIncomes = ParseAdditionalIncomes(element.Element("AdditionalIncomes"));
            }
            return new AdditionalPathModel();
        }

        private IList<IAdditionalIncome> ParseAdditionalIncomes(XElement element)
        {
            return new List<IAdditionalIncome>();
        }
    }
}
