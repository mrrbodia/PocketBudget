using PocketBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocketBudget.Controllers
{
    public class EducationController : Controller
    {
        [HttpPost]
        public ActionResult GetDegrees(PathViewModel path)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                if (path.EducationDegrees == null)
                    path.EducationDegrees = new EducationDegreesViewModel();
                path.EducationDegrees.Degrees = CreateDegreesModel(path.ProfessionSelection.SelectedProfession);
            }
            return View("~/Views/Home/Index.cshtml", path);
        }

        protected List<EducationDegreeViewModel> CreateDegreesModel(string professionId)
        {
            //TODO: degrees should be taken for profession
            var degrees = new List<EducationDegreeViewModel>();
            var bachelorDegree = new EducationDegreeViewModel() { IsReached = false, MinReachAge = 18, ReachedIn = 18, Title = "Бакалавр", IncomePercent = 0.10m };
            var masterDegree = new EducationDegreeViewModel() { IsReached = false, MinReachAge = 20, ReachedIn = 20, Title = "Магістр", IncomePercent = 0.15m };
            var doctoralDegree = new EducationDegreeViewModel() { IsReached = false, MinReachAge = 25, ReachedIn = 25, Title = "Докторська", IncomePercent = 0.20m };
            degrees.Add(bachelorDegree);
            degrees.Add(masterDegree);
            if (professionId == "1")
                degrees.Add(doctoralDegree);

            return degrees;
        }
    }
}
