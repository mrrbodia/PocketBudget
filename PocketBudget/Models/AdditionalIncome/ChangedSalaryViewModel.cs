using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class ChangedSalaryViewModel : AdditionalIncomeItemViewModel
    {
        public override string Title
        {
            get
            {
                return string.Format("Нова зарплатня");
            }
        }
    }
}