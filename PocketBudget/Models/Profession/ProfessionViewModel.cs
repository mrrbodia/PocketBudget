using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class ProfessionViewModel
    {
        public ProfessionViewModel()
        {
            Degrees = new List<EducationDegreeViewModel>();
        }

        [Display(Name = "Професія")]
        public string Title { get; set; }

        [UIHint("EducationDegrees")]
        public IList<EducationDegreeViewModel> Degrees { get; set; }

        public bool IsSelected { get; set; }
    }
}