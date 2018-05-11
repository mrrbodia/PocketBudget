using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PocketBudget.Models
{
    public class ProfessionViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Професія")]
        public string Title { get; set; }

        public bool IsSelected { get; set; }
    }
}