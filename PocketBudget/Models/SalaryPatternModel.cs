﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class SalaryPatternModel
    {
        public decimal IncomePerYear { get; set; }

        public double IncreasePercentage { get; set; }

        public int CurrentAge { get; set; }
        
        public int IncreaseTillAge { get; set; }

        public int ShowTillAge { get; set; }

        public int RetirementAge { get; set; }
    }
}