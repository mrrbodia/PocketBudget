using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class EducationModel
    {
        public EducationModel(string id, int from, decimal incomePercent, bool isHidden)
        {
            Id = id;
            From = from;
            IncomePercent = incomePercent;
            IsHidden = isHidden;
        }
        public bool IsHidden { get; set; }
        public string Id { get; set; }
        public int From { get; set; }
        public decimal IncomePercent { get; set; }
    }
}
