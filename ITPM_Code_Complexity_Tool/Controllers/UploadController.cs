﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using ITPM_Code_Complexity_Tool.Models;
using org.omg.IOP;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class UploadController : Controller
    {
        
        public string REDIRECT_PAGE = "UploadFile";
        public static Boolean multipleTrue = false;
        public static Object FILES_FROM_UPLOAD;

        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase[] files)
        {
            //Object FILES_FROM_UPLOAD = null;
            multipleTrue = false;
            var fi = files;

            if (fi != null)
            {

                System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/uploadedFiles/"));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                // Common FileNames object to use
                List<FileNames> fileNamesList = new List<FileNames>();

                Unzipper unzipper = new Unzipper(fileNamesList);// Declaring common obj

                try
                {
                    foreach (var file in files)
                    {

                        if (file.FileName != null)
                        {

                            string FILE_NAME = Path.GetFileName(file.FileName);
                            string SAVE_PATH = Path.Combine(Server.MapPath("~/uploadedFiles"), FILE_NAME);
                            file.SaveAs(SAVE_PATH);


                            // Checking whether if a zip file is being uploaded

                            if (System.IO.Path.GetExtension(file.FileName) == ".zip") // Uploded file is a zip file
                            {

                                unzipper.setFileName(file.FileName);
                                unzipper.Unzip();// Unzipping



                                SAVE_PATH = Path.Combine(Server.MapPath("~/uploadedFiles"), FILE_NAME);

                            }

                            if (System.IO.Path.GetExtension(FILE_NAME) != ".zip")
                            {
                                fileNamesList.Add(new FileNames(FILE_NAME)); // Add file name to the list
                            }

                        }
                        if (multipleTrue)
                        {
                            REDIRECT_PAGE = "MultipleFiles";
                        }


                        if (fileNamesList.Count > 1)// Check how many file are being uploaded
                        {
                            multipleTrue = true;
                            ViewBag.multipleTrue = multipleTrue;
                            REDIRECT_PAGE = "MultipleFiles";  // If multiple files are being uploaded, Redirect to this page
                        }
                        else if (fileNamesList.Count == 1)
                        {

                            REDIRECT_PAGE = "Tool_Home";
                        }

                        else // If no file is uploaded, 
                        {
                            REDIRECT_PAGE = "UploadFile";
                        }
                    }
                }
                catch (Exception e)
                {
                    return Redirect(REDIRECT_PAGE);
                }

                TempData["UPLOADED_FILES_LIST"] = fileNamesList;
                TempData.Keep("UPLOADED_FILES_LIST");
                ViewBag.names = fileNamesList;

            }
            return Redirect(REDIRECT_PAGE);

        }

        public ActionResult MultipleFiles()
        {
            if (TempData["UPLOADED_FILES_LIST"] != null)
            {
                FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            }

            ViewBag.FILES_FROM_UPLOAD = FILES_FROM_UPLOAD;
            System.Diagnostics.Debug.WriteLine("Controller Called In toolhome : " + FILES_FROM_UPLOAD);
            return View();

        }

        public ActionResult Tool_Home()
        {
            if (multipleTrue)
            {
                ViewBag.multipleTrue = 1;
            }
            else
            {
                ViewBag.multipleTrue = 0;
            }
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            return View();
        }


    }
}