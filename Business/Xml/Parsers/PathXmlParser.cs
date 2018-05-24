using Business.DomainModel.Active;
using Business.DomainModel.Cost;
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
            model.Id = element.Element("Id")?.Value;
            model.CurrentAge = short.Parse(element.Element("CurrentAge").Value);
            model.RetirementAge = short.Parse(element.Element("RetirementAge").Value);
            model.LifeExpectancy = short.Parse(element.Element("LifeExpectancy").Value);
            model.Salary = ParseSalary(element.Element("Salary"));
            model.Savings = ParseSavings(element.Element("SavingsModel"));
            model.Spendings = ParseSpendings(element.Element("SpendingsModel"));
            model.Pension = ParsePension(element.Element("PensionModel"));
            model.AdditionalPath = ParseAdditionalPath(element.Element("AdditionalPathModel"));
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
                    .Select(i => ParseAdditionalIncome(i))
                    .ToList();

                additionalPath.AdditionalCosts = element
                    .Element("AdditionalCosts")
                    .Elements("AdditionalCost")
                    .Select(c => ParseAdditionalCost(c))
                    .ToList();
            }
            return additionalPath;
        }

        private IAdditionalIncome ParseAdditionalIncome(XElement element)
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
                deposit.CurrencyId = element.Element("CurrencyId").Value;

                return deposit;
            }

            if (type.Equals(Constants.ChartLineType.Credit))
            {
                var sale = new Sale();
                sale.From = short.Parse(element.Element("From").Value);
                sale.IsActive = bool.Parse(element.Element("IsActive").Value);
                sale.IsHidden = bool.Parse(element.Element("IsHidden").Value);
                sale.Total = decimal.Parse(element.Element("Total").Value);
                sale.CurrencyId = element.Element("CurrencyId").Value;

                return sale;
            }

            return null;
        }

        private IAdditionalCost ParseAdditionalCost(XElement element)
        {
            var type = element.Element("LineType").Value;
            if (string.IsNullOrEmpty(type))
                return null;

            if (type.Equals(Constants.ChartLineType.Credit))
            {
                var credit = new Credit();
                credit.From = short.Parse(element.Element("From").Value);
                credit.IsHidden = bool.Parse(element.Element("IsHidden").Value);
                credit.Percentage = double.Parse(element.Element("Percentage").Value);
                credit.Total = decimal.Parse(element.Element("Total").Value);
                credit.Years = short.Parse(element.Element("Years").Value);
                credit.CurrencyId = element.Element("CurrencyId").Value;

                return credit;
            }

            if (type.Equals(Constants.ChartLineType.Purchase))
            {
                var purchase = new Purchase();
                purchase.From = short.Parse(element.Element("From").Value);
                purchase.IsHidden = bool.Parse(element.Element("IsHidden").Value);
                purchase.Total = decimal.Parse(element.Element("Total").Value);
                purchase.CurrencyId = element.Element("CurrencyId").Value;

                return purchase;
            }

            return null;
        }
    }
}
