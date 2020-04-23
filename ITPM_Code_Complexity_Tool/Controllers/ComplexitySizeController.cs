using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class ComplexitySizeController : Controller
    {
        // GET: ComplexitySizeController
        public ActionResult ComplexitySize()
        {
            //Model Class

            var detector = new Models.ComplexitySize();
            detector.SetFileName("pop.txt");
            detector.ProcessFile();
            var retVal = detector.showData();
            return View(retVal);
        }
        public ActionResult ComplexityVariables()
        {
            //Model Class

            var detector = new Models.ComplexityVariables();
            detector.SetFileName("pop.txt");
            detector.ProcessFile();
            var retVal = detector.showData();
            return View(retVal);
        }

        public ActionResult ComplexityMethods()
        {
            //Model Class

            var detector = new Models.ComplexityMethods();
            detector.SetFileName("pop.txt");
            detector.ProcessFile();
            var retVal = detector.showData();
            return View(retVal);
        }

    }
}