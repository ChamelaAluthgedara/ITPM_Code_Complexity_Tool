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

        static int globalVariable;
        static int localVariable;
        static int primitiveDataTypeVariable;
        static int CompositeDataTypeVariable;


        static int methodCReturnType;
        static int methodPeReturnType;
        static int methodPDataTypeParameter;
        static int methodCTypeParameter;
        static int methodVoid;




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
                ViewBag.totalNkw = detector.totalNkw;
                ViewBag.totalNid = detector.totalNid;
                ViewBag.totalNop = detector.totalNop;
                ViewBag.totalNnv = detector.totalNnv;
                ViewBag.totalNsl = detector.totalNsl;

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

            sizeVariableMethodsWeightTracker Weight = TempData["Weight"] as sizeVariableMethodsWeightTracker;
            ComplexityVariables w = new ComplexityVariables();
            ////set the weight 
            if (Weight != null)
            {
                globalVariable = Weight.variableGlobal;
                localVariable = Weight.variableLocal;
                primitiveDataTypeVariable = Weight.variablePrimitiveDataType;
                CompositeDataTypeVariable = Weight.variableCompotiteDataType;
            }


            w.getWeight(globalVariable, localVariable, primitiveDataTypeVariable, CompositeDataTypeVariable);



            string name = Request.Params["fileName"];
            var detector = new Models.ComplexityVariables();
            detector.SetFileName(name);
            detector.ProcessFile();
            var retVal = detector.showData();


            ViewBag.TotalCv = detector.totalCv;
            ViewBag.totalWeightDueToVScope = detector.totalWeightDueToVScope;
            ViewBag.totalNoPrimitiveDataTypeVariables = detector.totalNoPrimitiveDataTypeVariables;
            ViewBag.totalNoCompositeDataTypeVariables = detector.totalNoCompositeDataTypeVariables;


            return View(retVal);

        }


        // GET: ComplexityMethodsController
        [HttpGet]
        public ActionResult ComplexityMethods()
        {

            sizeVariableMethodsWeightTracker Weight = TempData["Weight"] as sizeVariableMethodsWeightTracker;
            ComplexityMethods w = new ComplexityMethods();
            ////set the weight 
            if (Weight != null)
            {

                methodPeReturnType = Weight.methodPrimitiveReturnType;
                methodCReturnType = Weight.methodCompositeReturnType;
                methodVoid = Weight.methodVoidReturnType;
                methodPDataTypeParameter = Weight.methodPrimitiveDataTypeParameter;
                methodCTypeParameter = Weight.methodCompositeDataTypeParameter;

            }

            w.getWeight(methodPeReturnType, methodCReturnType, methodVoid, methodPDataTypeParameter, methodCTypeParameter);


            string name = Request.Params["fileName"];
            var detector = new Models.ComplexityMethods();
            detector.SetFileName(name);
            detector.ProcessFile();
            var retVal = detector.showData();


            ViewBag.TotalCm = detector.totalCm;
            ViewBag.totalNcdtp = detector.totalNcdtp;
            ViewBag.totalNpdtp = detector.totalNpdtp;
            ViewBag.totalWmrt = detector.totalWmrt;

            return View(retVal);


        }


        //=======================================================

        public ActionResult SetWeightSize()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetWeightSize(sizeVariableMethodsWeightTracker sizeVariableMethodsWeightTracker, String fileName)
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
        //============================================================================================


        public ActionResult SetWeightVariables()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetWeightVariables(sizeVariableMethodsWeightTracker sizeVariableMethodsWeightTracker, String fileName)
        {


            //set the weight 
            sizeVariableMethodsWeightTracker weight = new sizeVariableMethodsWeightTracker()
            {

                variableGlobal = sizeVariableMethodsWeightTracker.variableGlobal,
                variableLocal = sizeVariableMethodsWeightTracker.variableLocal,
                variablePrimitiveDataType = sizeVariableMethodsWeightTracker.variablePrimitiveDataType,
                variableCompotiteDataType = sizeVariableMethodsWeightTracker.variableCompotiteDataType,

            };


            //return weight to index controller 
            TempData["Weight"] = weight;

            TempData.Keep("UPLOADED_FILES_LIST");
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            return RedirectToAction("ComplexityVariables", "ComplexitySizeVariablesMethods", new { fileName = fileName });
        }

        //=====================================================================


        public ActionResult SetWeightMethods()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetWeightMethods(sizeVariableMethodsWeightTracker sizeVariableMethodsWeightTracker, String fileName)
        {


            //set the weight 
            sizeVariableMethodsWeightTracker weight = new sizeVariableMethodsWeightTracker()
            {

                methodPrimitiveReturnType = sizeVariableMethodsWeightTracker.methodPrimitiveReturnType,
                methodCompositeReturnType = sizeVariableMethodsWeightTracker.methodCompositeReturnType,
                methodVoidReturnType = sizeVariableMethodsWeightTracker.methodVoidReturnType,
                methodPrimitiveDataTypeParameter = sizeVariableMethodsWeightTracker.methodPrimitiveDataTypeParameter,
                methodCompositeDataTypeParameter = sizeVariableMethodsWeightTracker.methodCompositeDataTypeParameter,

            };


            //return weight to index controller 
            TempData["Weight"] = weight;

            TempData.Keep("UPLOADED_FILES_LIST");
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            return RedirectToAction("ComplexityMethods", "ComplexitySizeVariablesMethods", new { fileName = fileName });
        }
    }
}