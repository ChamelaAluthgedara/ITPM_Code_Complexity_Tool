using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class ComplexityMethods
    {
        int lineNo = 0;
        public int Cm;
        public int totalCm;

        public int Wmrt;
        public int Npdtp;
        public int Ncdtp;

        public int Wprimitivedtp = 1;
        public int Wcompositedtp = 2;
        public int Wvoidtype = 0;
        public int WprimitiveDatatype = 1;
        public int WcompositeParameter = 2;


        private String FILE_NAME;

        List<CdueToMethod> completeList = new List<CdueToMethod>();

        public ComplexityMethods()  //Constructor
        {

        }

        public void getWeight(int methodPeReturnType, int methodCReturnType, int methodVoid, int methodPDataTypeParameter, int methodCTypeParameter)
        {
            Wprimitivedtp = methodPeReturnType;
            Wcompositedtp = methodCReturnType;
            Wvoidtype = methodVoid;
            WprimitiveDatatype = methodPDataTypeParameter;
            WcompositeParameter = methodCTypeParameter;
        }

        public void SetFileName(String fileName)
        {
            this.FILE_NAME = fileName;
        }

        public static string[] primitiveTypes = { "char", "byte", "short", "int", "long", "boolean", "float", "double" };


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
                        this.GetMethodCount(line);
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

        public void GetMethodCount(string line)
        {
            try
            {

                foreach (string singleRow in line.Split('\n'))
                {
                    for (int i = 0; i < primitiveTypes.Length; i++)
                    {

                        System.Diagnostics.Debug.WriteLine("This is row line: " + singleRow);

                        if (singleRow.Contains("public") && singleRow.Contains(primitiveTypes[i]) && singleRow.Contains("(") && !singleRow.Contains("args"))
                        {
                            Npdtp++;
                        }
                        if (singleRow.Contains("public") && singleRow.Contains("void") && singleRow.Contains("(") && !singleRow.Contains("args"))
                        {
                            Npdtp++;
                        }
                        if (singleRow.Contains("public") && singleRow.Contains("static") && singleRow.Contains(primitiveTypes[i]) && singleRow.Contains("(") && !singleRow.Contains("args"))
                        {
                            Npdtp++;
                        }
                        if (singleRow.Contains("public") && singleRow.Contains("static") && singleRow.Contains("void") && singleRow.Contains("(") && !singleRow.Contains("args"))
                        {
                            Npdtp++;
                        }
                    }
                    if (singleRow.Contains("public") && singleRow.Contains("static") && singleRow.Contains("main") && singleRow.Contains("(") && singleRow.Contains("args"))
                    {
                        Ncdtp++;
                    }
                }


            }
            catch (Exception)
            {

            }
            lineNo++;
            Cm = Wmrt + (Wprimitivedtp * Npdtp) + (Wcompositedtp * Ncdtp);
            totalCm = totalCm + Cm;
            completeList.Add(new CdueToMethod(lineNo, line, Ncdtp, Npdtp, Wmrt, Cm));
            Npdtp = 0;
            Ncdtp = 0;
            Cm = 0;
            CdueToMethod c = new CdueToMethod(this.totalCm);

        }

        public List<CdueToMethod> showData()
        {
            return completeList;
        }

    }
}


