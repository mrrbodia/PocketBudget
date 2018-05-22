using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class EducationModel
    {
        public EducationModel(bool isHidden)
        {
            IsHidden = isHidden;
        }
        public bool IsHidden { get; set; }
        public int From { get; set; }
        public List<EducationDegreeModel> EducationDegrees { get; set; }
        public decimal GetIncomePercent(int from, int inAge)
        {
            return EducationDegrees?.FirstOrDefault(x => from <= inAge && x.To > inAge)?.IncomePercent ?? 0;
        }
    }
}
