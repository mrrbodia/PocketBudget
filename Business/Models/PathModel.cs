
namespace Business.Models
{
    public class PathModel
    {
        public short CurrentAge { get; set; }

        public short RetirementAge { get; set; }

        public short LifeExpectancy { get; set; }

        public SalaryModel Salary { get; set; }

        public decimal Savings { get; set; }

        public decimal Spendings { get; set; }

        public AdditionalPathModel AdditionalPath { get; set; }
    }
}
