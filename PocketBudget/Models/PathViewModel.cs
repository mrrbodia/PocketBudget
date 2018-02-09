using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class PathViewModel
    {
        public PathViewModel()
        {
            Salary = new SalaryModel();
            Savings = new SavingsModel();
            Spendings = new SpendingsModel();
        }

        public short CurrentAge { get; set; }

        public short RetirementAge { get; set; }

        public short LifeExpectancy { get; set; }

        public SalaryModel Salary { get; set; }

        public SavingsModel Savings { get; set; }

        public SpendingsModel Spendings { get; set; }
    }
}