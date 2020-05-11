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

        CdueToVariables c = new CdueToVariables();

        static int WeightGlobalVariable;
        static int WeightLocalVariable;
        static int WeightPrimitiveDataTypeVariable;
        static int WeightCompositeDataTypeVariable;
        static int WeightDueToVScope = 1;

        int NoPrimitiveDataTypeVariables = 0;
        int NoCompositeDataTypeVariables = 0;
        int Cv = 0;

        private String FILE_NAME;

        public static string[] primitiveDataTypes = { "char", "byte", "short", "int", "long", "boolean", "float", "double", "String" };

        public static string[] compositeDataTypes = { "ArrayList ", "List", "[" };


        List<CdueToVariables> completeList = new List<CdueToVariables>();


        public void getWeight(int globalVariable, int localVariable, int primitiveDataTypeVariable, int CompositeDataTypeVariable)
        {
            WeightGlobalVariable = globalVariable;
            WeightLocalVariable = localVariable;
            WeightPrimitiveDataTypeVariable = primitiveDataTypeVariable;
            WeightCompositeDataTypeVariable = CompositeDataTypeVariable;

        }


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
            

           // Console.WriteLine("Im from GetVariablesCount Called:: " + c.WeightGlobalVariable);
            try
            {
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries
                try
                {
                    foreach (string rowLine in line.Split('\n'))
                    {

                        for (int i = 0; i < primitiveDataTypes.Length; i++)
                        {
                            if (rowLine.Contains("public"))
                            {
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("static") && rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("static") && !rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                            }
                        }

                        for (int i = 0; i < primitiveDataTypes.Length; i++)
                        {
                            if (rowLine.Contains("private"))
                            {
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("static") && rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("static") && !rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                            }

                        }

                        for (int i = 0; i < primitiveDataTypes.Length; i++)
                        {
                            if (rowLine.Contains("protected"))
                            {

                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("static") && rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                                if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains("static") && !rowLine.Contains("=") && rowLine.Contains(";"))
                                {
                                    NoPrimitiveDataTypeVariables++;
                                }
                            }
                        }

                        for (int i = 0; i < primitiveDataTypes.Length; i++)
                        {

                            if (rowLine.Split('.').Contains(primitiveDataTypes[i]) && !rowLine.Split('.').Contains(".") && !rowLine.Split('.').Contains("=") && rowLine.Split('.').Contains("("))
                            {
                                NoPrimitiveDataTypeVariables++;
                            }

                            if (rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains(".") && !rowLine.Contains("=") && rowLine.Contains(";"))
                            {
                                NoPrimitiveDataTypeVariables++;
                            }
                            if (rowLine.Contains("static") && rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains(".") && !rowLine.Contains("=") && rowLine.Contains(";"))
                            {
                                NoPrimitiveDataTypeVariables++;
                            }
                            if (rowLine.Contains("static") && rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains(".") && rowLine.Contains("=") && rowLine.Contains(";"))
                            {
                                NoPrimitiveDataTypeVariables++;
                            }
                            if (rowLine.Contains("static") && rowLine.Contains("final") && rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains(".") && !rowLine.Contains("=") && rowLine.Contains(";"))
                            {
                                NoPrimitiveDataTypeVariables++;
                            }
                            if (rowLine.Contains("static") && rowLine.Contains("final") && rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains(".") && rowLine.Contains("=") && rowLine.Contains(";"))
                            {
                                NoPrimitiveDataTypeVariables++;
                            }
                            if (rowLine.Contains("final") && rowLine.Contains(primitiveDataTypes[i]) && !rowLine.Contains(".") && !rowLine.Contains("=") && rowLine.Contains(";"))
                            {
                                NoPrimitiveDataTypeVariables++;
                            }

                        }

                    }
                }
                catch (Exception)
                {

                }

                try
                {
                    foreach (string rowLine in line.Split('\n'))
                    {

                        for (int i = 0; i < compositeDataTypes.Length; i++)
                        {
                            if (rowLine.Contains(compositeDataTypes[i]) && rowLine.Contains("=") && !rowLine.Contains("new") && rowLine.Contains(";") && !rowLine.Contains("."))
                            {
                                NoCompositeDataTypeVariables++;
                            }
                        }
                        if (rowLine.Contains("=") && rowLine.Contains("new") && rowLine.Contains("(") && rowLine.Contains(";") && !rowLine.Contains("."))
                        {
                            NoCompositeDataTypeVariables++;
                        }
                    }
                }
                catch (Exception)
                {

                }


                lineNo++;


                Cv = (WeightDueToVScope * ((WeightPrimitiveDataTypeVariable * NoPrimitiveDataTypeVariables) + (WeightCompositeDataTypeVariable * NoCompositeDataTypeVariables)));
                totalCv = totalCv + Cv;
                completeList.Add(new CdueToVariables(lineNo, line, WeightDueToVScope, NoPrimitiveDataTypeVariables, NoCompositeDataTypeVariables, Cv));
                NoPrimitiveDataTypeVariables = 0;
                NoCompositeDataTypeVariables = 0;
                Cv = 0;
                CdueToVariables c = new CdueToVariables(this.totalCv);
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


