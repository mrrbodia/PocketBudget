﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class PensionViewModel
    {
        [Display(Name = "Пенсія")]
        public decimal Amount { get; set; }
    }
}