using Business.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PocketBudget.Models
{
    public class PathViewModel
    {
        public PathViewModel()
        {
            Salary = new SalaryViewModel();
            ProfessionSelection = new ProfessionSelectionViewModel();
            Salary.SalaryPeriods = new List<SalaryPeriodViewModel>();
            Savings = new SavingsViewModel();
            Spendings = new SpendingsViewModel();
            Pension = new PensionViewModel();
            EducationDegrees = new EducationDegreesViewModel();
        }

        [Display(Name = "Ваш вік")]
        [Required(ErrorMessage = "Введіть ваш вік")]
        [Range(0, 80, ErrorMessage = "Доступні значення з {1} до {2}")]
        public short CurrentAge { get; set; }

        [Display(Name = "Вихід на пенсію")]
        [Required(ErrorMessage = "Введіть вік виходу на пенсію")]
        [Range(0, 80, ErrorMessage = "Доступні значення з {1} до {2}")]
        public short RetirementAge { get; set; }

        [Display(Name = "Тривалість життя")]
        [Required(ErrorMessage = "Введіть тривалість життя")]
        [Range(0, 100, ErrorMessage = "Доступні значення з {1} до {2}")]
        public short LifeExpectancy { get; set; }

        [UIHint("Salary")]
        public SalaryViewModel Salary { get; set; }

        [UIHint("Profession")]
        public ProfessionSelectionViewModel ProfessionSelection { get; set; }

        [UIHint("EducationDegrees")]
        [Display(Name = "Освіта")]
        public EducationDegreesViewModel EducationDegrees { get; set; }

        [UIHint("Savings")]
        public SavingsViewModel Savings { get; set; }

        [UIHint("Spendings")]
        public SpendingsViewModel Spendings { get; set; }

        [UIHint("Pension")]
        public PensionViewModel Pension { get; set; }

        public AdditionalPathViewModel AdditionalPath { get; set; }

        public bool IsValid()
        {
            return Enumerable.Range(50, 90).Contains(this.RetirementAge)
                && Enumerable.Range(55, 100).Contains(this.LifeExpectancy)
                && this.RetirementAge < this.LifeExpectancy
                && this.Salary != null
                && this.Savings != null
                && this.Spendings != null
                && this.Pension != null;
        }
    }
}