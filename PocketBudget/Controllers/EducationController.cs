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
            var bachelorDegree = new EducationDegreeViewModel() { IsReached = true, MinReachAge = 18, ReachedIn = 18, Title = "Бакалавр" };
            var masterDegree = new EducationDegreeViewModel() { IsReached = false, MinReachAge = 20, ReachedIn = 20, Title = "Магістр" };
            degrees.Add(bachelorDegree);
            if (professionId == "1")
                degrees.Add(masterDegree);

            return degrees;
        }
    }
}
