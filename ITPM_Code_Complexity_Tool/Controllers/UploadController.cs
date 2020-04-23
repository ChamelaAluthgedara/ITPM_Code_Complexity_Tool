using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool.Controllers
{
    public class UploadController : Controller
    {
        // GET: FileUpload
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
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            try
            {
                if (!file.Equals(null))
                {
                 
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName =  "userUploadFile" + extension;
                    string _path = Path.Combine(Server.MapPath("~/uploadedFiles"), fileName);
                    file.SaveAs(_path);

                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("ComplexitySize", "ComplexitySize");
            }
            catch(Exception e)
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

    }



}

