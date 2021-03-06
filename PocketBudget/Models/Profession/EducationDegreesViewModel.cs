﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class EducationDegreesViewModel
    {
        public bool IsHidden { get; set; }

        [Display(Name = "Освіта")]
        public List<EducationDegreeViewModel> Degrees { get; set; }
    }
}