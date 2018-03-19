﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models.AdditionalCost
{
    public class CreditViewModel : AdditionalCostItemViewModel
    {
        public float Percentage { get; set; }

        public short Years { get; set; }

        public bool IsActive { get; set; }
    }
}