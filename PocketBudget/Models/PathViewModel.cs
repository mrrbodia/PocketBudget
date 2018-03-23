using Business.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PocketBudget.Models
{
    public class PathViewModel
    {
        public PathViewModel()
        {
            Salary = new SalaryViewModel();
            Savings = new SavingsViewModel();
            Spendings = new SpendingsViewModel();
            Pension = new PensionViewModel();
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