using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class AllFactorsController : Controller
    {
        // GET: ComplexitySizeController
        [HttpGet]
        public ActionResult AllFactors()
        {

            string name = Request.Params["fileName"];
            var detector = new Models.AllFactors_Processor();
            detector.SetFileName(name);
            detector.ProcessFile();
            var retVal = detector.showData();
           // ViewBag.TotalCs = detector.totalCS;
            return View(retVal);


        }

    }
}