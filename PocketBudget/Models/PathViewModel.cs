using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class PathViewModel
    {
        public short CurrentAge { get; set; }

        public short RetirementAge { get; set; }

        public short LifeExpectancy { get; set; }

        public decimal Savings { get; set; }

        public decimal Spendings { get; set; }
    }
}