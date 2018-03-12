using Business.Models;
using System.Linq;

namespace PocketBudget.Models
{
    public class PathViewModel
    {
        public PathViewModel()
        {
            Salary = new SalaryModel();
            Savings = new SavingsModel();
            Spendings = new SpendingsModel();
            Pension = new PensionModel();
        }

        public short CurrentAge { get; set; }

        public short RetirementAge { get; set; }

        public short LifeExpectancy { get; set; }

        public SalaryModel Salary { get; set; }

        public SavingsModel Savings { get; set; }

        public SpendingsModel Spendings { get; set; }

        public PensionModel Pension { get; set; }

        public bool IsValid()
        {
            return Enumerable.Range(50, 90).Contains(this.RetirementAge)
                && Enumerable.Range(55, 100).Contains(this.LifeExpectancy)
                && this.RetirementAge < this.LifeExpectancy
                && this.Salary != null
                && this.Savings != null
                && this.Spendings != null
                && this.Pension != null;
        }
    }
}