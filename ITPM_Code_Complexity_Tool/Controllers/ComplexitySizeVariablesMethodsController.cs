using ITPM_Code_Complexity_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class ComplexitySizeVariablesMethodsController : Controller
    {
        static int sizeKeyword;
        static int sizeIdentifers;
        static int sizeOperators;
        static int sizeNumericValues;
        static int sizeStringLiteral;

        // GET: ComplexitySizeController
        [HttpGet]
        public ActionResult ComplexitySize(String fileName)
        {

            try
            {
                sizeVariableMethodsWeightTracker Weight = TempData["Weight"] as sizeVariableMethodsWeightTracker;
                ComplexitySize w = new ComplexitySize();
                ////set the weight 
                if (Weight != null)
                {
                    sizeKeyword = Weight.sizeKeyword;
                    sizeIdentifers = Weight.sizeIdentifers;
                    sizeOperators = Weight.sizeOperators;
                    sizeNumericValues = Weight.sizeNumericValues;
                    sizeStringLiteral = Weight.sizeStringLiteral;
                }

                w.getWeight(sizeKeyword, sizeIdentifers, sizeOperators, sizeNumericValues, sizeStringLiteral);

                string name = Request.Params["fileName"];
                name = fileName;
                var detector = new Models.ComplexitySize();
                detector.SetFileName(name);
                detector.ProcessFile();
                var retVal = detector.showData();
                ViewBag.TotalCs = detector.totalCS;
                return View(retVal);
            }
            catch (Exception)
            {

                throw;
            }

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

        public ActionResult SetWeight()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetWeight(sizeVariableMethodsWeightTracker sizeVariableMethodsWeightTracker, String fileName)
        {


            //set the weight 
            sizeVariableMethodsWeightTracker weight = new sizeVariableMethodsWeightTracker()
            {

                sizeKeyword = sizeVariableMethodsWeightTracker.sizeKeyword,
                sizeIdentifers = sizeVariableMethodsWeightTracker.sizeIdentifers,
                sizeOperators = sizeVariableMethodsWeightTracker.sizeOperators,
                sizeNumericValues = sizeVariableMethodsWeightTracker.sizeNumericValues,
                sizeStringLiteral = sizeVariableMethodsWeightTracker.sizeStringLiteral,

            };


            //return weight to index controller 
            TempData["Weight"] = weight;

            TempData.Keep("UPLOADED_FILES_LIST");
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            return RedirectToAction("ComplexitySize", "ComplexitySizeVariablesMethods", new { fileName = fileName });
        }

    }
}