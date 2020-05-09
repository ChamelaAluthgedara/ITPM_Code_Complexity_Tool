using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class InheritanceController : Controller
    {
        // GET: Inheritance
        [HttpGet]
        public ActionResult Inheritance_viewer()
        {
            //Model Class
            string name = Request.Params["fileName"];
            var detector = new Models.Inheritance_Detector();
            detector.SetFileName(name);
            detector.ProcessFile();
            var retVal = detector.showData();
            return View(retVal);
        }
    }
}