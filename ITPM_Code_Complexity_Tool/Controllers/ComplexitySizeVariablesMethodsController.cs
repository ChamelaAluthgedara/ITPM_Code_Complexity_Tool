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
        [HttpGet]
        public ActionResult ComplexitySize()
        {

            string name = Request.Params["fileName"];
            var detector = new Models.ComplexitySize();
            detector.SetFileName(name);
            detector.ProcessFile();
            var retVal = detector.showData();
            ViewBag.TotalCs = detector.totalCS;
            return View(retVal);


        }
        // GET: ComplexityVariablesController
        [HttpGet]
        public ActionResult ComplexityVariables()
        {

            string name = Request.Params["fileName"];
            var detector = new Models.ComplexityVariables();
            detector.SetFileName(name);
            detector.ProcessFile();
            var retVal = detector.showData();
            ViewBag.TotalCv = detector.totalCv;
            return View(retVal);

        }
        // GET: ComplexityMethodsController
        [HttpGet]
        public ActionResult ComplexityMethods()
        {
            string name = Request.Params["fileName"];
            var detector = new Models.ComplexityMethods();
            detector.SetFileName(name);
            detector.ProcessFile();
            var retVal = detector.showData();
            ViewBag.TotalCm = detector.totalCm;
            return View(retVal);


        }

    }
}