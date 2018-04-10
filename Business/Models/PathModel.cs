
namespace Business.Models
{
    public class PathModel
    {
        public string Id {get;set;}

        public short CurrentAge { get; set; }

        public short RetirementAge { get; set; }

        public short LifeExpectancy { get; set; }

        public SalaryModel Salary { get; set; }

        public SavingsModel Savings { get; set; }

        public SpendingsModel Spendings { get; set; }

        public AdditionalPathModel AdditionalPath { get; set; }

        public PensionModel Pension { get; set; }
    }
}
