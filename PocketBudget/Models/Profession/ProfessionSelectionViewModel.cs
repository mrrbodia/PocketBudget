using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class ProfessionSelectionViewModel
    {
        public ProfessionSelectionViewModel()
        {
            Professions = new List<ProfessionViewModel>();
        }

        [UIHint("Professions")]
        [Display(Name = "Професія")]
        public IList<ProfessionViewModel> Professions { get; set; }
    }
}