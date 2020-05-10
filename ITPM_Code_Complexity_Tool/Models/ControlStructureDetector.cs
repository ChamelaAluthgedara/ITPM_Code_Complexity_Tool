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

        //Set the file name
        public void SetFileName(String fileName)
        {
            this.FILE_NAME = fileName;
        }


        public void ProcessFile(ControlStructureWeight weight)
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

                        this.ControStructureDetect(line, weight);
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

        //Detect the all control Structure in code lines
        public void ControStructureDetect(String line, ControlStructureWeight weight)
        {

            ControlStructureWeight Weight = new ControlStructureWeight()
            {
                ifElseIfWeight = weight.ifElseIfWeight,
                forWileDoWhileWeight = weight.forWileDoWhileWeight,
                SwitchWeight = weight.SwitchWeight,
                CaseWeight = weight.CaseWeight

            };


            foreach (string row in line.Split('\n'))
            {


                //Check if line has "If" , "ifelse" Conditions
                if (row.Contains("if(") || row.Contains("if (") || row.Contains("else if(") ||
                    row.Contains("else if ("))
                {
                    this.wtcs = Weight.ifElseIfWeight;
                    this.NC = this.NC + 1;
                    this.Ccs = (this.wtcs * this.NC) + this.Ccpps;


                }
                //Check if line has "for" , "while" Conditions

                else if (row.Contains("for(") || row.Contains("while("))
                {
                    this.wtcs = Weight.forWileDoWhileWeight;
                    this.NC = this.NC + 1;
                    this.Ccs = (this.wtcs * this.NC) + this.Ccpps;
                }
                //Check if line has "switch" Conditions

                else if (row.Contains("switch (") || row.Contains("switch("))
                {
                    this.wtcs = Weight.SwitchWeight;
                    this.NC = this.NC + 1;
                    this.Ccs = (this.wtcs * this.NC) + this.Ccpps;


                }

                //Check if line has "case" Conditions

                else if (row.Contains("case"))
                {
                    this.wtcs = Weight.CaseWeight;
                    this.NC = this.NC + 1;
                    this.Ccs = (this.wtcs * this.NC) + this.Ccpps;

                }
                //No control structure method in line
                else
                {

                    this.Ccs = (this.wtcs * this.NC) + this.Ccpps;

                }
                //set Ccsspps value
                if (this.Ccs != 0 && this.NewCcspps != 0)
                {
                    this.Ccpps = this.NewCcspps;
                    this.Ccs = (this.wtcs * this.NC) + this.Ccpps;

                }

                if (this.Ccs != 0 && (row.Contains("switch(") || row.Contains("switch (")))
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

        //return the List of Call control structure model set values
        public List<Controlstructure> result()
        {

            return this.consList;
        }





    }
}