using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class ComplexitySizeVariablesMethodsController : Controller
    {
        // GET: ComplexitySizeController
        public ActionResult ComplexitySize()
        {
            var detector = new Models.ComplexitySize();
            detector.ProcessFile();
            var retVal = detector.showData();
            ViewBag.TotalCs = detector.totalCS;
            return View(retVal );
        }
        // GET: ComplexityVariablesController
        public ActionResult ComplexityVariables()
        {
            var detector = new Models.ComplexityVariables();
            detector.ProcessFile();
            var retVal = detector.showData();
            ViewBag.TotalCv = detector.totalCv;
            return View(retVal);
        }
        // GET: ComplexityMethodsController
        public ActionResult ComplexityMethods()
        {
            var detector = new Models.ComplexityMethods();
            detector.ProcessFile();
            var retVal = detector.showData();
            ViewBag.TotalCm = detector.totalCm;
            return View(retVal);
        }

    }
}