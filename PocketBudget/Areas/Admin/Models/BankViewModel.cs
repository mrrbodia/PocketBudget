using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Areas.Admin.Models
{
    public class BankViewModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        public string Name { get; set; }

        public short Rating { get; set; }
    }
}