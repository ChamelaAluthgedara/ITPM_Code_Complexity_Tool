using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_2020.Controllers
{
    public class InheritanceController : Controller
    {
        // GET: Inheritance
        public ActionResult Inheritance_viewer()
        {
            //Model Class

            var detector = new Models.Inheritance_Detector();
            detector.SetFileName("DaysPerMonth.java");
            detector.ProcessFile();
            var retVal = detector.showData();
            return View(retVal);
        }
       
    }
}