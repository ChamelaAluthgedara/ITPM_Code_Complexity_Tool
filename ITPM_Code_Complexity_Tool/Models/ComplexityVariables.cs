using java.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class ComplexityVariables
    {
        int lineNo = 0;
        public int totalCv;

        int WeightDueToVScope = 1;
        int Wpdv = 1;
        int Wcdtv = 1;



        int NoPrimitiveDataTypeVariables = 0;
        int NoCompositeDataTypeVariables = 0;
        int Cv = 0;

        private String FILE_NAME;

        public static string[] primitiveDataTypes = {

            "int",
            "float",
            "double",
            "char",
            "string",
            "long",
            "Boolean",


        };

        public static string[] compositeDataTypes = {
            "[",
            "int[ ",
            "float[ ",
            "double[ ",
            "char[ ",
            "string[ ",
            "long[ ",
            "Boolean[ ",
            "ArrayList ",

             "int[",
            "float[",
            "double[",
            "char[",
            "string[",
            "long[",
            "Boolean[",

             "int [",
            "float [",
            "double [",
            "char [",
            "string [",
            "long [",
            "Boolean [",

             "int [ ",
            "float [ ",
            "doube [ ",
            "char [ ",
            "string [ ",
            "long [ ",
            "Boolean [ ",


        };


        List<CdueToVariables> completeList = new List<CdueToVariables>();

        public ComplexityVariables()  //Constructor
        {

        }

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
                        //this.Detect(line);
                        this.GetVariablesCount(line);
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

        public void GetVariablesCount(string line)
        {
           try
            {
                string[] words = line.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries
                try
                {
                    for (int j = 0; j < primitiveDataTypes.Length; j++)
                    {
                        for (int i = 0; i < words.Length; i++)
                        {

                            if (words[i] == primitiveDataTypes[j])
                            {
                                
                                System.Diagnostics.Debug.WriteLine("line: " + words[i]);

                                NoPrimitiveDataTypeVariables++;
                            }
                        }
                    }
                }
                finally
                {

                }

                //try
                //{
                //    for (int j = 0; j < compositeDataTypes.Length; j++)
                //    {
                //        for (int i = 0; i < words.Length; i++)
                //        {
                //            if (words[i] + " " == compositeDataTypes[j])
                //            {
                //                NoCompositeDataTypeVariables++;
                //            }
                //        }
                //    }
                //}

                //finally
                //{

                //}

                lineNo++;
                Cv = (WeightDueToVScope * ((Wpdv * NoPrimitiveDataTypeVariables) + ( Wcdtv * NoCompositeDataTypeVariables ) ));
                totalCv = totalCv + Cv;
                completeList.Add(new CdueToVariables(lineNo, line, WeightDueToVScope, NoPrimitiveDataTypeVariables, NoCompositeDataTypeVariables, Cv));
                NoPrimitiveDataTypeVariables = 0;
                NoCompositeDataTypeVariables = 0;
                Cv = 0;
                CdueToVariables c = new CdueToVariables(this.totalCv);
                System.Diagnostics.Debug.WriteLine("totalCv: " + totalCv);
            }
            finally
            {

            }
        }

        public List<CdueToVariables> showData()
        {
            return completeList;
        }

    }
}


