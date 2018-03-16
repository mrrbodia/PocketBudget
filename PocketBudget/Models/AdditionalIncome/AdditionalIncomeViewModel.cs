﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class AdditionalIncomeViewModel
    {
        public AdditionalIncomeViewModel()
        {
            Deposits = new List<DepositViewModel>();
        }

        public int? From { get; set; }

        public IEnumerable<DepositViewModel> Deposits { get; set; }
    }
}