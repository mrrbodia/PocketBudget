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
        [HttpGet]
        public ActionResult GetDegrees(string professionId)
        {
            return View("_EducationDegrees", CreateDegreesModel(professionId));
        }

        protected IList<EducationDegreeViewModel> CreateDegreesModel(string professionId)
        {
            //TODO: degrees should be taken for profession
            var degrees = new List<EducationDegreeViewModel>();

            var bachelorDegree = new EducationDegreeViewModel() { IsReached = false, MinReachAge = 18, ReachedIn = 18, Title = "Бакалавр" };
            var masterDegree = new EducationDegreeViewModel() { IsReached = false, MinReachAge = 20, ReachedIn = 20, Title = "Магістр" };
            degrees.Add(bachelorDegree);
            if (professionId == "1")
                degrees.Add(masterDegree);

            return degrees;
        }
    }
}
