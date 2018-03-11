
using System.Linq;
namespace Business.Models
{
    public class PathModel
    {
        public short CurrentAge { get; set; }

        public short RetirementAge { get; set; }

        public short LifeExpectancy { get; set; }

        public SalaryModel Salary { get; set; }

        public SavingsModel Savings { get; set; }

        public SpendingsModel Spendings { get; set; }

        public AdditionalPathModel AdditionalPath { get; set; }

        public PensionModel Pension { get; set; }

        public bool IsValid()
        {
            return Enumerable.Range(50, 90).Contains(this.RetirementAge)
                && Enumerable.Range(55, 100).Contains(this.LifeExpectancy)
                && this.RetirementAge < this.LifeExpectancy
                && this.CurrentAge != null
                && this.Salary.Amount != null
                && this.Savings.Amount != null
                && this.Spendings.Amount != null
                && this.Pension.Amount != null;
        }
    }
}
