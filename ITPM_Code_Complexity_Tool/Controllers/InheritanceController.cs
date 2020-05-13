using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class InheritanceController : Controller
    {

        private int INHERITED_NO_CLASS = 0;
        private int INHERITED_ONE_CLASS = 1;   // Weights
        private int INHERITED_TWO_CLASSES = 2;
        private int INHERITED_THREE_CLASSES = 3;
        private int INHERITED_MORE_THAN_FOUR_CLASSES = 4;



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

        public ActionResult setWeight()
        {
            ViewData["NO_CLASS"] = this.INHERITED_NO_CLASS;
            ViewData["ONE_CLASS"] = this.INHERITED_ONE_CLASS;
            ViewData["TWO_CLASS"] = this.INHERITED_TWO_CLASSES;
            ViewData["THREE_CLASS"] = this.INHERITED_THREE_CLASSES;
            ViewData["MORE_THAN_FOUR"] = this.INHERITED_MORE_THAN_FOUR_CLASSES;
            
            return View();
        }

        public ActionResult setIt()
        {
            int zero =int.Parse( Request.Params["ZERO"] );
            int one = int.Parse(Request.Params["ONE"]);
            int two = int.Parse(Request.Params["TWO"]);
            int three = int.Parse(Request.Params["THREE"]);
            int four = int.Parse(Request.Params["MORE_FOUR"]);


            return RedirectToAction("Inheritance_viewer");
        }


    }
}