using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.IO.Compression;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class Unzipper
    {
        private string PATH_TO_ZIP;
        private string PATH_TO_UNZIP;
        private string ZIP_FILE_NAME;

        private List<FileNames> returnList;

        public Unzipper(List<FileNames> retList)
        {
            this.returnList = retList;
            this.ZIP_FILE_NAME = "";

        }

        public void setFileName(string fileName)
        {
            this.ZIP_FILE_NAME = fileName;
            this.PATH_TO_ZIP = HttpContext.Current.Server.MapPath("~/uploadedFiles/" + this.ZIP_FILE_NAME);
            //    this.PATH_TO_UNZIP = HttpContext.Current.Server.MapPath("~/uploadedFiles/" + "EXTRACTED_" + this.ZIP_FILE_NAME);
            this.PATH_TO_UNZIP = HttpContext.Current.Server.MapPath("~/uploadedFiles/");
        }


        public void Unzip()
        {

            using (ZipArchive archive = ZipFile.OpenRead(PATH_TO_ZIP))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string newname = Path.GetFileName(entry.ToString());
                    entry.ExtractToFile(PATH_TO_UNZIP + newname);
                    returnList.Add(new FileNames(newname));
                }

            }

        }



    }
}