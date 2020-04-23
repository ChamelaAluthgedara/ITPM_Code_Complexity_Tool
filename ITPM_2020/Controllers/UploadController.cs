using ITPM_2020.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_2020.Controllers
{
    public class UploadController : Controller
    {


        String RET_FILE_NAME = "";
        //public List<ITPM_2020.Models.FileNames> fileNames = new List<FileNames>(); //This List is used to return uploded File Names
            
            
            

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

            // Common FileNames object to use
            List<FileNames> fileNamesList = new List<FileNames>();
            String REDIRECT_PAGE = "";

            if ( files.Length > 1)// Check how many file are being uploaded
            {
                REDIRECT_PAGE = "MultipleFiles";  // If multiple files are being uploaded, Redirect to this page
            }
            else
            {
                REDIRECT_PAGE = "Tool_Home";
            }
            
            
            foreach (var file in files)
            {

                if (file.FileName != null)
                {
                    string FILE_NAME = Path.GetFileName(file.FileName);                 
                    string SAVE_PATH = Path.Combine(Server.MapPath("~/uploadedFiles"), FILE_NAME);
                    file.SaveAs(SAVE_PATH);
                    fileNamesList.Add(new FileNames(FILE_NAME)); // Add file name to the list
                    


                }

                else // If no file is uploaded, 
                {
                    REDIRECT_PAGE = "UploadFile";
                }
                

            }

            TempData["UPLOADED_FILES_LIST"] = fileNamesList;
            return Redirect(REDIRECT_PAGE);

        }

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //This method is used to upload multiple files simaltaneously
        /*
                [HttpPost]
                public ActionResult UploadFile(HttpPostedFileBase[] files)
                {
                    fileNames.Add(new FileNames("Upload File"));


                    foreach (var file in files)
                    {

                        if (file.FileName != null)
                        {
                            string FILE_NAME = Path.GetFileName(file.FileName);
                            fileNames.Add(new FileNames(FILE_NAME));
                            string SAVE_PATH = Path.Combine(Server.MapPath("~/uploadedFiles"), FILE_NAME);

                            file.SaveAs(SAVE_PATH);
                            fileNames.Add(new FileNames(FILE_NAME));
                            fileNames.Add(new FileNames("It works bro "));

                        }

                        else // If no file is uploaded, 
                        {
                            fileNames.Add(new FileNames("No File"));
                            fileNames.Add(new FileNames("It does not work bro "));
                        }
                        fileNames.Add(new FileNames("In foreach loop "));

                    }
                    fileNames.Add(new FileNames("In Function "));

                    return Redirect("MultipleFiles");

                }


            */



/*
        public List<FileNames> showFileNames() //This will return names of uploded files
        {
            fileNames.Add(new FileNames("show File "));
            return fileNames;
        }
*/

      
        public ActionResult MultipleFiles()
        {
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            return View();

        }

     

        public ActionResult Tool_Home()
        {
            ViewBag.FILES_FROM_UPLOAD = TempData["UPLOADED_FILES_LIST"];
            return View();
        }






    }


   



    }

