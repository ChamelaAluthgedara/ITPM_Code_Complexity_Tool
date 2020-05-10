using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.Services.Description;
using ITPM_Code_Complexity_Tool.Models;
using Microsoft.Ajax.Utilities;


namespace ITPM_Code_Complexity_Tool.Controllers
{

    public class ControlStrctureController : Controller
    {

        //Read all lines in file and Calculate Control Structure, Return List
        [HttpGet]
        public ActionResult Index(String FileNames)
        {

            //get weight pass from setweight page
            ControlStructureWeight Weight = TempData["Weight"] as ControlStructureWeight;

            ControlStructureWeight weight = new ControlStructureWeight()
            {
                ifElseIfWeight = Weight.ifElseIfWeight,
                forWileDoWhileWeight = Weight.forWileDoWhileWeight,
                SwitchWeight = Weight.SwitchWeight,
                CaseWeight = Weight.CaseWeight

            };



            ControlStructureDetector controlStructure = new ControlStructureDetector();

            //set filename pass from URL
            controlStructure.SetFileName(FileNames);

            //calculate conrol structure in files
            controlStructure.ProcessFile(weight);

            List<Controlstructure> controlstructuresList = new List<Controlstructure>();

            //retun list
            controlstructuresList = controlStructure.result();

            return View(controlstructuresList);

        }


        public ActionResult SetWeight()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SetWeight(ControlStructureWeight controlStructureWeight, String fileName)
        {


            //set the weight 
            ControlStructureWeight weight = new ControlStructureWeight()
            {

                ifElseIfWeight = controlStructureWeight.ifElseIfWeight,
                forWileDoWhileWeight = controlStructureWeight.forWileDoWhileWeight,
                SwitchWeight = controlStructureWeight.SwitchWeight,
                CaseWeight = controlStructureWeight.CaseWeight

            };


            //return weight to index controller 
            TempData["Weight"] = weight;
            return RedirectToAction("Index", "ControlStrcture", new { FileNames = fileName });
        }


    }
}