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

        public string SelectedProfession { get; set; }

        [UIHint("Professions")]
        [Display(Name = "Професія")]
        public IList<ProfessionViewModel> Professions { get; set; }
    }
}