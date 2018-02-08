
namespace Business.Models
{
    public class PathModel
    {
        public short CurrentAge { get; set; }

        public short RetirementAge { get; set; }

        public short LifeExpectancy { get; set; }

        public decimal Savings { get; set; }

        public decimal Spendings { get; set; }

        public AdditionalPathModel AdditionalPath { get; set; }

        //TODO: first main fork should be added as default
        //public List<ChartLine> Lines { get; set; }
    }
}
