using ITPM_Code_Complexity_Tool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    //    public class UploadController : Controller
    //    {
    //        // GET: FileUpload
    //        public ActionResult Index()
    //        {
    //            return View();
    //        }

    //        [HttpGet]
    //        public ActionResult UploadFile()
    //        {

    //            return View();
    //        }

    //        [HttpPost]
    //        public ActionResult UploadFile(HttpPostedFileBase file)
    //        {

    //                if (file.ContentLength > 0)
    //                {

    //                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
    //                    string extension = Path.GetExtension(file.FileName);
    //                    fileName =  "userUploadFile" + extension;
    //                    string _path = Path.Combine(Server.MapPath("~/uploadedFiles"), fileName);
    //                    file.SaveAs(_path);

    //                    ViewBag.Message = "File Uploaded Successfully!!";
    //                    return RedirectToAction("Index", "Function");
    //                }
    //                else
    //                {
    //                    ViewBag.Message = "File upload failed!!";
    //                    return RedirectToAction("UploadFile", "UploadFile");
    //                }


    //        }

    //    }



    //}
    public class UploadController : Controller
    {



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



            Unzipper unzipper = new Unzipper(fileNamesList);// Declaring common obj

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


                if (fileNamesList.Count > 1)// Check how many file are being uploaded
                {
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

            TempData["UPLOADED_FILES_LIST"] = fileNamesList;
            TempData.Keep("UPLOADED_FILES_LIST");
            ViewBag.names = fileNamesList;
            return Redirect(REDIRECT_PAGE);

        }




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
