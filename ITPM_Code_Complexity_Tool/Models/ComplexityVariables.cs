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
        public int totalWeightDueToVScope;
        public int totalNoPrimitiveDataTypeVariables;
        public int totalNoCompositeDataTypeVariables;


        public static Boolean globalVariable;
        public Boolean detected = false;

        CdueToVariables c = new CdueToVariables();

        static int WeightGlobalVariable;
        static int WeightLocalVariable;
        static int WeightPrimitiveDataTypeVariable;
        static int WeightCompositeDataTypeVariable;
        static int WeightDueToVScope;

        int NoPrimitiveDataTypeVariables = 0;
        int NoCompositeDataTypeVariables = 0;
        int Cv = 0;
        public int CvouterAccess;

        private String FILE_NAME;

        public static string[] primitiveDataTypes = { "int ", "char ", "byte ", "short ", "long ", "boolean ", "float ", "double ", "String " };

        public static string[] compositeDataTypes = { "ArrayList ", "List", "[" };

        List<string> allMethods = new List<string>();


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
                //string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries
                try
                {
                    foreach (string rowLine in line.Split('\n'))
                    {


                        if (rowLine.Contains("public") || rowLine.Contains("private") || rowLine.Contains("protected"))
                        {
                            if (globalVariable == false)
                            {
                                if (rowLine.Contains("class") && !rowLine.Contains("(") && !rowLine.Contains(")"))
                                {
                                    globalVariable = true;
                                }
                            }

                            if (globalVariable)
                            {
                                for (int i = 0; i < primitiveDataTypes.Length; i++)
                                {
                                    if ((rowLine.Contains("void")) && rowLine.Contains("(") && !rowLine.Contains(";") && rowLine.Contains(")"))
                                    {
                                        globalVariable = false;
                                    }
                                }
                            }

                            //in class variable without in methods

                            if (globalVariable)
                            {
                                for (int i = 0; i < primitiveDataTypes.Length; i++)
                                {


                                    //primitive global
                                    if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains(";"))
                                    {
                                        if (rowLine.Contains(","))
                                        {
                                            int count = rowLine.Split(',').Length - 1;
                                            // System.Diagnostics.Debug.WriteLine("Global primitive split count: " + count);
                                            NoPrimitiveDataTypeVariables = count + 1;
                                            WeightDueToVScope = WeightGlobalVariable;
                                        }
                                        else
                                        {
                                            NoPrimitiveDataTypeVariables++;
                                            WeightDueToVScope = WeightGlobalVariable;
                                        }

                                    }


                                    //composite global

                                    if (!(rowLine.Contains("int") || rowLine.Contains("double") || rowLine.Contains("char") || rowLine.Contains("byte") || rowLine.Contains("short") || rowLine.Contains("String") || rowLine.Contains("long") || rowLine.Contains("boolean") || rowLine.Contains("float")) && rowLine.Contains(";"))
                                    {

                                        //System.Diagnostics.Debug.WriteLine("global composite  row: " + rowLine + " " );


                                        if (rowLine.Contains(","))
                                        {
                                            int count = rowLine.Split(',').Length - 1;

                                            if (!detected)
                                            {
                                                NoCompositeDataTypeVariables = count + 1;
                                                WeightDueToVScope = WeightGlobalVariable;
                                            }
                                            detected = true;
                                        }
                                        else
                                        {
                                            if (!detected)
                                            {
                                                NoCompositeDataTypeVariables++;
                                                WeightDueToVScope = WeightGlobalVariable;
                                            }
                                            detected = true;
                                        }

                                    }

                                }
                                detected = false;


                            }

                            // END OF in class variable without in methods

                            else
                            {
                                //local composite variables
                                for (int i = 0; i < 1; i++)
                                {
                                    if (rowLine.Contains("=") && rowLine.Contains(";") && rowLine.Contains("(") && !(rowLine.Contains("int") || rowLine.Contains("double") || rowLine.Contains("char") || rowLine.Contains("byte") || rowLine.Contains("short") || rowLine.Contains("String") || rowLine.Contains("long") || rowLine.Contains("boolean") || rowLine.Contains("float")))
                                    {

                                        // System.Diagnostics.Debug.WriteLine("LOCAL inside composite split........... : ");
                                        string firstFragment = rowLine.Split('=').First();
                                        //System.Diagnostics.Debug.WriteLine("left word: " + firstFragment);

                                        if (firstFragment.Contains(primitiveDataTypes[i]))
                                        {
                                            if (rowLine.Contains(","))
                                            {

                                                int count = rowLine.Split(',').Length - 1;
                                                System.Diagnostics.Debug.WriteLine("LOCAL composite split count: " + count);
                                                if (!detected)
                                                {
                                                    NoCompositeDataTypeVariables = count + 1;
                                                    WeightDueToVScope = WeightLocalVariable;
                                                    detected = true;
                                                }

                                            }
                                            else
                                            {
                                                if (!detected)
                                                {
                                                    NoCompositeDataTypeVariables++;
                                                    WeightDueToVScope = WeightLocalVariable;
                                                    detected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                detected = false;


                                // local primitive variable
                                for (int i = 0; i < primitiveDataTypes.Length; i++)
                                {
                                    //System.Diagnostics.Debug.WriteLine("split count in local primitive: " + "global state:: " + globalVariable);
                                    // System.Diagnostics.Debug.WriteLine("line " + rowLine);
                                    if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains(";"))
                                    {
                                        if (rowLine.Contains(","))
                                        {

                                            int count = rowLine.Split(',').Length - 1;
                                            //  System.Diagnostics.Debug.WriteLine("split count in local primitive: " + count +"\n line: " + rowLine);
                                            NoPrimitiveDataTypeVariables = count + 1;
                                            WeightDueToVScope = WeightLocalVariable;

                                        }
                                        else
                                        {

                                            // System.Diagnostics.Debug.WriteLine("split count in local primitive: " + "\n line: " + rowLine);
                                            NoPrimitiveDataTypeVariables++;
                                            WeightDueToVScope = WeightLocalVariable;
                                        }

                                    }

                                }
                            }


                        }

                        else
                        {
                            if (globalVariable == false)
                            {




                                //local composite variables
                                for (int i = 0; i < 1; i++)
                                {
                                    if (rowLine.Contains("=") && rowLine.Contains(";") && rowLine.Contains("(") && !(rowLine.Contains("int") || rowLine.Contains("double") || rowLine.Contains("char") || rowLine.Contains("byte") || rowLine.Contains("short") || rowLine.Contains("String") || rowLine.Contains("long") || rowLine.Contains("boolean") || rowLine.Contains("float")))
                                    {

                                        // System.Diagnostics.Debug.WriteLine("LOCAL inside composite split........... : ");
                                        string firstFragment = rowLine.Split('=').First();
                                        //System.Diagnostics.Debug.WriteLine("left word: " + firstFragment);

                                        if (firstFragment.Contains(primitiveDataTypes[i]))
                                        {
                                            if (rowLine.Contains(","))
                                            {

                                                int count = rowLine.Split(',').Length - 1;
                                                System.Diagnostics.Debug.WriteLine("LOCAL composite split count: " + count);
                                                if (!detected)
                                                {
                                                    NoCompositeDataTypeVariables = count + 1;
                                                    WeightDueToVScope = WeightLocalVariable;
                                                    detected = true;
                                                }

                                            }
                                            else
                                            {
                                                if (!detected)
                                                {
                                                    NoCompositeDataTypeVariables++;
                                                    WeightDueToVScope = WeightLocalVariable;
                                                    detected = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                detected = false;


                                // local primitive variable
                                for (int i = 0; i < primitiveDataTypes.Length; i++)
                                {
                                    //System.Diagnostics.Debug.WriteLine("split count in local primitive: " + "global state:: " + globalVariable);
                                    // System.Diagnostics.Debug.WriteLine("line " + rowLine);
                                    if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains(";"))
                                    {
                                        if (rowLine.Contains(","))
                                        {

                                            int count = rowLine.Split(',').Length - 1;
                                            //  System.Diagnostics.Debug.WriteLine("split count in local primitive: " + count +"\n line: " + rowLine);
                                            NoPrimitiveDataTypeVariables = count + 1;
                                            WeightDueToVScope = WeightLocalVariable;

                                        }
                                        else
                                        {
                                            if (!rowLine.Contains("int"))
                                            {
                                                // System.Diagnostics.Debug.WriteLine("split count in local primitive: " + "\n line: " + rowLine);
                                                NoPrimitiveDataTypeVariables++;
                                                WeightDueToVScope = WeightLocalVariable;
                                            }

                                        }

                                    }

                                }




                                // local primitive variable
                                for (int i = 0; i < primitiveDataTypes[i].Length; i++)
                                {
                                    //System.Diagnostics.Debug.WriteLine("split count in local primitive: " + "global state:: " + globalVariable);
                                    //System.Diagnostics.Debug.WriteLine("line " + rowLine);
                                    if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains(";"))
                                    {
                                        string firstFragment = rowLine.Split('=').First();
                                        //System.Diagnostics.Debug.WriteLine("left word: " + firstFragment);

                                        if (firstFragment.Contains(primitiveDataTypes[i]))
                                        {
                                            if (rowLine.Contains(","))
                                            {
                                                int count = rowLine.Split(',').Length - 1;
                                                // System.Diagnostics.Debug.WriteLine("split count in local primitive: " + count + "\n line: " + rowLine);
                                                if (!detected)
                                                {
                                                    NoPrimitiveDataTypeVariables = count + 1;
                                                    WeightDueToVScope = WeightLocalVariable;
                                                    detected = true;
                                                }
                                            }
                                            else
                                            {
                                                if (!detected)
                                                {
                                                    // System.Diagnostics.Debug.WriteLine("split count in local primitive: " + "\n line: " + rowLine);
                                                    NoPrimitiveDataTypeVariables++;
                                                    WeightDueToVScope = WeightLocalVariable;
                                                    detected = true;
                                                }
                                            }
                                        }
                                    }


                                }
                                detected = false;

                                //local composite variables
                                for (int i = 0; i < primitiveDataTypes.Length; i++)
                                {
                                    if (rowLine.Contains("=") && rowLine.Contains(";") && rowLine.Contains("(") && !(rowLine.Contains("int") || rowLine.Contains("double") || rowLine.Contains("char") || rowLine.Contains("byte") || rowLine.Contains("short") || rowLine.Contains("String") || rowLine.Contains("long") || rowLine.Contains("boolean") || rowLine.Contains("float")))
                                    {


                                        // System.Diagnostics.Debug.WriteLine("LOCAL inside composite split........... : ");
                                        string firstFragment = rowLine.Split('=').First();



                                        char[] delimiters = new char[] { ' ', '\r', '\n' };
                                        int wordCount = firstFragment.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                                        System.Diagnostics.Debug.WriteLine("left word: " + wordCount);

                                        if (!firstFragment.Contains(primitiveDataTypes[i]))
                                        {
                                            if (wordCount >= 2)
                                            {
                                                if (rowLine.Contains(","))
                                                {

                                                    int count = rowLine.Split(',').Length - 1;
                                                    System.Diagnostics.Debug.WriteLine("LOCAL composite split count: " + count);
                                                    if (!detected)
                                                    {
                                                        NoCompositeDataTypeVariables = count + 1;
                                                        WeightDueToVScope = WeightLocalVariable;
                                                        detected = true;
                                                    }

                                                }
                                                else
                                                {
                                                    if (!detected)
                                                    {
                                                        NoCompositeDataTypeVariables++;
                                                        WeightDueToVScope = WeightLocalVariable;
                                                        detected = true;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                                detected = false;



                            }

                            if (globalVariable)
                            {
                                for (int i = 0; i < primitiveDataTypes.Length; i++)
                                {
                                    //primitive global
                                    if (rowLine.Contains(primitiveDataTypes[i]) && rowLine.Contains(";") && !(rowLine.Contains("public") || rowLine.Contains("private") || rowLine.Contains("protected")))
                                    {
                                        if (rowLine.Contains(","))
                                        {
                                            int count = rowLine.Split(',').Length - 1;
                                            // System.Diagnostics.Debug.WriteLine("Global primitive split count: " + count);
                                            NoPrimitiveDataTypeVariables = count + 1;
                                            WeightDueToVScope = WeightGlobalVariable;
                                        }
                                        else
                                        {
                                            NoPrimitiveDataTypeVariables++;
                                            WeightDueToVScope = WeightGlobalVariable;
                                        }

                                    }

                                }
                            }
                        }
                        detected = false;
                    }
                }
                catch (Exception)
                {

                }
                lineNo++;
                Cv = (WeightDueToVScope * ((WeightPrimitiveDataTypeVariable * NoPrimitiveDataTypeVariables) + (WeightCompositeDataTypeVariable * NoCompositeDataTypeVariables)));



                completeList.Add(new CdueToVariables(lineNo, line, WeightDueToVScope, NoPrimitiveDataTypeVariables, NoCompositeDataTypeVariables, Cv));

                totalCv = totalCv + Cv;
                totalWeightDueToVScope = totalWeightDueToVScope + WeightDueToVScope;
                System.Diagnostics.Debug.WriteLine("totalWeightDueToVScope:: " + totalWeightDueToVScope);
                totalNoPrimitiveDataTypeVariables = totalNoPrimitiveDataTypeVariables + NoPrimitiveDataTypeVariables;
                totalNoCompositeDataTypeVariables = totalNoCompositeDataTypeVariables + NoCompositeDataTypeVariables;


                WeightDueToVScope = 0;
                NoPrimitiveDataTypeVariables = 0;
                NoCompositeDataTypeVariables = 0;
                //System.Diagnostics.Debug.WriteLine("Due to Variables: Im in Variables " + Cv);
                CvouterAccess = Cv;
                Cv = 0;

                CdueToVariables c = new CdueToVariables(this.totalCv, this.totalWeightDueToVScope, this.totalNoPrimitiveDataTypeVariables, this.totalNoCompositeDataTypeVariables);
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


