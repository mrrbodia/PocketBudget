﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class DepositViewModel : AdditionalIncomeItemViewModel
    {
        public float Percentage { get; set; }

        public short Years { get; set; }

        public bool IsActive { get; set; }
    }
}