using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class InheritanceController : Controller
    {

        private int INHERITED_NO_CLASS;
        private int INHERITED_ONE_CLASS = 1;
        private int INHERITED_TWO_CLASSES = 2;
        private int INHERITED_THREE_CLASSES = 3;
        private int INHERITED_MORE_THAN_FOUR_CLASSES = 4;

       
        private Models.Inheritance_Detector detector = new Models.Inheritance_Detector();


        // GET: Inheritance
        [HttpGet]
        public ActionResult Inheritance_viewer()
        {
            //Model Class
            string NAME = "";
            //TempData["FILE"] = "";

           
            if (Request.Params["fileName"] != null)
            {
                NAME = Request.Params["fileName"];
                //TempData["FILE"] = NAME;
            }
            else
            {
                NAME = TempData["FILE"].ToString();
            }

            //Setting Weight variables
            if( Request.Params["ONE"] != null)
            {

                int NO_CLASS = int.Parse( Request.Params["ZERO"] );
                int ONE_CLASS = int.Parse(Request.Params["ONE"]);
                int TWO_CLASSES = int.Parse(Request.Params["TWO"]);
                int THREE_CLASSES = int.Parse(Request.Params["THREE"]);
                int FOUR_CLASSES = int.Parse(Request.Params["FOUR"]);

                detector.setValOfWeight(NO_CLASS, ONE_CLASS, TWO_CLASSES, THREE_CLASSES, FOUR_CLASSES);

                // Set current vals of this obje
                this.INHERITED_NO_CLASS = NO_CLASS;
                this.INHERITED_ONE_CLASS = ONE_CLASS;
                this.INHERITED_TWO_CLASSES = TWO_CLASSES;
                this.INHERITED_THREE_CLASSES = THREE_CLASSES;
                this.INHERITED_MORE_THAN_FOUR_CLASSES = FOUR_CLASSES;


            }
            




        // var detector = new Models.Inheritance_Detector();
        detector.SetFileName( NAME );
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

            string name2 = Request.Params["fileName"];
            TempData["FILE"] = name2;

            return View();
        }

        public ActionResult setIt()
        {
            int no  =int.Parse( Request.Params["ZERO"] );
            int one = int.Parse(Request.Params["ONE"]);
            int two = int.Parse(Request.Params["TWO"]);
            int three = int.Parse(Request.Params["THREE"]);
            int four = int.Parse(Request.Params["MORE_FOUR"]);
            string name2 = Request.Params["fileName"];
            if( name2 == null)
            {
                name2 = TempData["FILE"].ToString();
            }
            else
            {
                TempData["FILE"] = name2;
            }
         

            return Redirect("Inheritance_viewer?fileName=" + name2 + "&ZERO=" + no + "&ONE="+one+"&TWO="+two+"&THREE="+three+"&FOUR="+four );



        }


    }
}