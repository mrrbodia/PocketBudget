using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class EducationDegreeViewModel
    {
        public string Title { get; set; }

        [Display(Name = "Досягти у віці")]
        public int ReachedIn { get; set; }

        [UIHint("Checkbox")]
        public bool IsReached { get; set; }

        public int MinReachAge { get; set; }

        public decimal IncomePercent { get; set; }
    }
}