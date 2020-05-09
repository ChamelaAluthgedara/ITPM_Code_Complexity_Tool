using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class ControlStructureDetector
    {
        private String FILE_NAME = string.Empty;

        private int wtcs = 0, NC = 0, Ccpps = 0, Ccs = 0, NewCcspps = 0;
        private int LineNo = 0;
        List<int> CcppsList = new List<int>();
        List<Controlstructure> consList = new List<Controlstructure>();


        public void SetFileName(String fileName)
        {
            this.FILE_NAME = fileName;
        }


        public void ProcessFile()
        {




            try
            {

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                string PATH_TO_UPLOADED_FILE = HttpContext.Current.Server.MapPath("~/uploadedFiles/" + this.FILE_NAME);
                string line;
                using (StreamReader sr = new StreamReader(PATH_TO_UPLOADED_FILE))
                {

                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {

                        this.ControStructureDetect(line);
                    }

                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }



        }


        public void ControStructureDetect(String line)
        {

            foreach (string row in line.Split('\n'))
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


        public List<Controlstructure> result()
        {

            return this.consList;
        }





    }
}