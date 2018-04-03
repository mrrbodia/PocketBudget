using Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Xml.Linq;

namespace Business.DataProviders
{
    public class XMLDataProvider
    {
        private const string examplesPath = "/content/xml/PathModels.xml";
        private const string defaultModelPath = "/content/xml/DefaultPathModel.xml";

        public IList<PathModel> GetPathModels(string filePath = examplesPath)
        {
            if (!File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                return new List<PathModel>();

            var xml = XDocument.Load(@HttpContext.Current.Server.MapPath(filePath));
            var elements = xml.Elements("PathModel")
                .Select(element => ParsePath(element))
                .ToList();

            return elements;
        }

        public PathModel GetDefaultPathModel()
        {
            return GetPathModels(defaultModelPath).FirstOrDefault();
        }

        private PathModel ParsePath(XElement element)
        {
            var model = new PathModel();
            model.CurrentAge = short.Parse(element.Element("CurrentAge").Value);
            model.RetirementAge = short.Parse(element.Element("RetirementAge").Value);
            model.LifeExpectancy = short.Parse(element.Element("LifeExpectancy").Value);
            model.Salary = ParseSalary(element.Element("Salary"));
            model.Savings = ParseSavings(element.Element("SavingsModel"));
            model.Spendings = ParseSpendings(element.Element("SpendingsModel"));
            model.Pension = ParsePension(element.Element("PensionModel"));
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
    }
}
