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
        // GET: ControlStrcture



        private string filename = string.Empty;
        private string filepath = string.Empty;
        private int wtcs = 0, NC = 0, Ccpps = 0, Ccs = 0, NewCcspps = 0;
        private int LineNo = 0;
        List<int> CcppsList = new List<int>();
        //List<string> bracketList = new List<string>();

        public ActionResult Index()
        {


            return View();
        }



        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            List<Controlstructure> consList = new List<Controlstructure>();


            try
            {
                if (file.ContentLength > 0)
                {
                    this.filename = Path.GetFileName(file.FileName);
                    this.filepath = Path.Combine(Server.MapPath("~/UploadedFiles"), this.filename);
                    file.SaveAs(this.filepath);

                }

                ViewBag.Message = "Upload success";


            }
            catch (Exception e)
            {
                return HttpNotFound();
            }


            if (this.filename != null && this.filepath != null)
            {

                string csvdata = System.IO.File.ReadAllText(this.filepath);

                foreach (string row in csvdata.Split('\n'))
                {
                    //if (row.Contains("{"))
                    //{
                    //    bracketList.Add("{");
                    //}
                    //else if(row.Contains("}"))
                    //{
                    //    bracketList.Add("}");
                    //}



                    if (row.Contains("if(") || row.Contains("else if(") || row.Contains("else"))
                    {
                        this.wtcs = 2;
                        this.NC = this.NC + 1;
                        this.Ccs = (this.wtcs * this.NC) + this.Ccpps;


                    }
                    else if (row.Contains("for(") || row.Contains("while("))
                    {
                        this.wtcs = 3;
                        this.NC = this.NC + 1;
                        this.Ccs = (this.wtcs * this.NC) + this.Ccpps;
                    }
                    else
                    {

                        this.Ccs = (this.wtcs * this.NC) + this.Ccpps;

                    }

                    if (this.Ccs != 0 && this.NewCcspps != 0)
                    {
                        this.Ccpps = this.NewCcspps;
                        this.Ccs = (this.wtcs * this.NC) + this.Ccpps;

                    }

                    if (this.Ccs != 0)
                    {
                        CcppsList.Add(Ccs);
                        this.NewCcspps = CcppsList[(CcppsList.Count) - 1];

                    }



                    consList.Add(new Controlstructure
                    {
                        LineNO = this.LineNo + 1,
                        ProgramStatment = row,
                        Wtcs = this.wtcs,
                        NC = this.NC,
                        Ccpps = this.Ccpps,
                        Ccs = this.Ccs
                    });

                    this.LineNo++;
                    this.wtcs = 0;
                    this.NC = 0;
                    this.Ccs = 0;
                    this.Ccpps = 0;

                }


            }
            else
            {
                return HttpNotFound();
            }




            return View(consList);



        }

        [HttpPost]
        public ActionResult Reset()
        {

            return RedirectToAction("Index");
        }





    }
}