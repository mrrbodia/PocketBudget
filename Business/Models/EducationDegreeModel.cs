using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class EducationDegreeModel
    {
        public EducationDegreeModel(string id, int from, decimal incomePercent)
        {
            Id = id;
            From = from;
            IncomePercent = incomePercent;
        }
        public string Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public decimal IncomePercent { get; set; }
    }
}
