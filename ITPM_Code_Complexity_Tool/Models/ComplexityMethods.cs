using Antlr4.Runtime.Misc;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using java.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{

    public class ComplexityMethods
    {
        CdueToMethod c = new CdueToMethod();

        public static Boolean voidDetected;

        public Boolean detected = false;
        public int k = 0;

        int lineNo = 0;
        public int CmouterAccess;
        public int Cm;
        public int totalCm;
        public int Wmrt;
        public int Npdtp;
        public int Ncdtp;

        static int Wprimitivedtp;
        static int Wcompositedtp;
        static int Wvoidtype;
        static int WprimitiveDatatype;
        static int WcompositeParameter;

        public int totalNcdtp;
        public int totalNpdtp;
        public int totalWmrt;


        List<string> primitiveTypesArray = new List<string>();

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

            //System.Diagnostics.Debug.WriteLine("Im from GetVariablesCount Called:: " + methodPeReturnType);
        }

        public void SetFileName(String fileName)
        {
            this.FILE_NAME = fileName;
        }

        public static string[] primitiveTypes = { "char", "byte", "short", "int", "long", "boolean", "float", "double", "String" };


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
                    CdueToMethod c2 = new CdueToMethod();
                    c2.voidDetected = false;

                    if (singleRow.Contains("public") || singleRow.Contains("private") || singleRow.Contains("protected"))
                    {

                        if (singleRow.Contains("void") && singleRow.Contains("(") && !singleRow.Contains("args"))
                        {
                            string regularExpressionPattern = @"\((.*?)\)";
                            Regex re = new Regex(regularExpressionPattern);
                            foreach (Match m in re.Matches(singleRow))
                            {
                                if (m.Value.Equals("()"))
                                {
                                    System.Diagnostics.Debug.WriteLine("void detected " + m.Value);
                                    Wmrt++;
                                    Wmrt = Wmrt * Wvoidtype;
                                    voidDetected = true;
                                }
                            }
                        }
                    }
                    foreach (string v in primitiveTypes)
                    {

                        if (singleRow.Contains("public") || singleRow.Contains("private") || singleRow.Contains("protected") || singleRow.Contains("static"))
                        {
                            if (singleRow.Contains(v))
                            {
                                //////method return type
                                if (singleRow.Contains(v) && singleRow.Contains("(") && !singleRow.Contains("args"))
                                {
                                    string regularExpressionPattern = @"\((.*?)\)";

                                    Regex re = new Regex(regularExpressionPattern);

                                    foreach (Match m in re.Matches(singleRow))
                                    {
                                        if (m.Value.Equals("()"))
                                        {
                                            // System.Diagnostics.Debug.WriteLine("empty () detected: " + m.Value);
                                            Wmrt++;
                                            Wmrt = Wmrt * Wprimitivedtp;

                                        }
                                    }
                                }


                                if (singleRow.Contains(v))
                                {
                                    //////method return type
                                    if (!singleRow.Contains(v) && singleRow.Contains("(") && !singleRow.Contains("void") && !singleRow.Contains("args"))
                                    {
                                        string regularExpressionPattern = @"\((.*?)\)";

                                        Regex re = new Regex(regularExpressionPattern);

                                        foreach (Match m in re.Matches(singleRow))
                                        {
                                            if (m.Value.Equals("()"))
                                            {
                                                // System.Diagnostics.Debug.WriteLine("empty () detected: " + m.Value);
                                                Wmrt++;
                                                Wmrt = Wmrt * Wcompositedtp;
                                            }
                                        }
                                    }
                                }


                                if (singleRow.Contains(v))
                                {
                                    //////method return type
                                    if (singleRow.Contains("void") && singleRow.Contains("(") && singleRow.Contains("main") && singleRow.Contains("args") && singleRow.Contains("String"))
                                    {
                                        string regularExpressionPattern = @"\((.*?)\)";

                                        Regex re = new Regex(regularExpressionPattern);

                                        foreach (Match m in re.Matches(singleRow))
                                        {
                                            if (!m.Value.Equals("()"))
                                            {
                                                // System.Diagnostics.Debug.WriteLine("empty () detected: " + m.Value);
                                                Ncdtp++;
                                            }
                                        }
                                    }
                                }


                                // primitive data type parameter with void
                                if (singleRow.Contains("void") && singleRow.Contains("(") && !singleRow.Contains("args"))
                                {
                                    string regularExpressionPattern = @"\((.*?)\)";

                                    Regex re = new Regex(regularExpressionPattern);
                                    string lineWords = singleRow;
                                    foreach (Match m in re.Matches(lineWords))
                                    {
                                        if (!m.Value.Equals("()"))
                                        {
                                            string valk = m.Value;

                                            char[] spearator = { ' ', '(', ')' };
                                            Int32 count = 10;

                                            // Using the Method 
                                            String[] strlist = valk.Split(spearator, count, StringSplitOptions.None);

                                            foreach (String s in strlist)
                                            {

                                                if (s == v)
                                                {
                                                    primitiveTypesArray.Add(s);
                                                    foreach (string word in primitiveTypes)
                                                    {
                                                        if (primitiveTypesArray[k] == word)
                                                        {
                                                            Npdtp++;
                                                        }
                                                    }
                                                    k++;
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {


                                // composite data type parameter with void type
                                if (!singleRow.Contains(v) && singleRow.Contains("void") && singleRow.Contains("(") && !singleRow.Contains("args"))
                                {
                                    string regularExpressionPattern = @"\((.*?)\)";
                                    Regex re = new Regex(regularExpressionPattern);

                                    foreach (Match m in re.Matches(singleRow))
                                    {
                                        if (!m.Value.Equals("()"))
                                        {
                                            if (!m.Value.Contains(v))
                                            {
                                                if (!detected)
                                                {
                                                    // System.Diagnostics.Debug.WriteLine("composite parameter " + m.Value);
                                                    Ncdtp++;
                                                }
                                                detected = true;
                                            }
                                        }
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
            System.Diagnostics.Debug.WriteLine(".Void detected: " + voidDetected);
            lineNo++;
            Cm = Wmrt + (WprimitiveDatatype * Npdtp) + (WcompositeParameter * Ncdtp);

            totalCm = totalCm + Cm;
            totalNcdtp = totalNcdtp + Ncdtp;
            totalNpdtp = totalNpdtp + Npdtp;
            totalWmrt = totalWmrt + Wmrt;

            completeList.Add(new CdueToMethod(lineNo, line, Ncdtp, Npdtp, Wmrt, voidDetected, Cm));
            voidDetected = false;
            Wmrt = 0;
            Npdtp = 0;
            Ncdtp = 0;
            CmouterAccess = Cm;
            Cm = 0;
            CdueToMethod c = new CdueToMethod(this.totalCm, this.totalNcdtp, this.totalNpdtp, this.totalWmrt);

        }

        public List<CdueToMethod> showData()
        {
            return completeList;
        }

    }
}


