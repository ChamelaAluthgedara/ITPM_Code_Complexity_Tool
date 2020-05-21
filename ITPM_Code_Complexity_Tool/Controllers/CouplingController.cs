using ITPM_Code_Complexity_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class CouplingController : Controller
    {
        // GET: Coupling
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Coupling_viewer()
        {
            //Model Class
            //string name = Request.Params["fileName"];
            //var detector = new Models.Inheritance_Detector();
            //detector.SetFileName(name);
            //detector.ProcessFile();
            //var retVal = detector.showData();
            //return View(retVal);


            string name = Request.Params["fileName"];
            var coupling = new Models.Coupling();
            coupling.SetFileName(name);
            var couplingList = coupling.coupling("");

            return View(couplingList);


        }
    }
}