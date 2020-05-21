using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
using javax.swing.text;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using System.Text.RegularExpressions;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class ComplexitySize
    {
        int lineNo = 0;
        public string codeLine;
        int keywordCount = 0;
        int operatorCount = 0;
        int numricalCount = 0;
        int identifires = 0;
        int stringLiteral = 0;
        public int cs = 0;


        public int totalNkw;
        public int totalNid;
        public int totalNop;
        public int totalNnv;
        public int totalNsl;
        public int totalCS;

        public int csOuterAccess;

        public static int weightKeyword;
        public static int WeightIdentifers;
        public static int WeightOperators;
        public static int WeightNumericalVal;
        public static int WeightStringLiteral;


        List<string> operatorsHolder = new List<string>();
        public int k = 0;
        //CdueToSize sMw = new CdueToSize();

        public void getWeight(int keyword, int identifer, int operators, int numericalVal, int literalString)
        {
            weightKeyword = keyword;
            WeightIdentifers = identifer;
            WeightOperators = operators;
            WeightNumericalVal = numericalVal;
            WeightStringLiteral = literalString;

        }

        private String FILE_NAME;

        public static string[] numericalArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static string[] identifiresArray = { "class", "return", "main", "System", "out", "print", "printf" };
        public static string[] wordAray = { "class", "static", "extends", "java", "public", "private", "protected", "void", "true", "else", "default", "return", "null", "break", "this" };
        public static string[] operatorAray = { ".", "+", "-", "*", "/", "%", "%.", "++","--",  "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!",
         "|", "^", "~", "<<", ">>", ">>>", "<<<", "->", ",", "::", "+=", "-=", "*=", "/=", " = ", "=", ">>>=", "|=", "&=", "%=", "<<=", ">>=",
         "^="};
        public String[] controlStrucures = { "if", "while", "do", "switch", "for" };

        List<CdueToSize> completeList = new List<CdueToSize>();
        public ComplexitySize()  //Constructor
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
                        this.GetKeywordCount(line.Trim());
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

        public void GetKeywordCount(string line)
        {
            string[] wordOp = line.Trim().Split(new char[] { ' ', '\r', '\n', ';', '"', '"', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 'w', 'x', 'y', 'z' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

            try
            {
                foreach (string singleRow in line.Split('\n'))
                {
                    for (int i = 0; i < wordAray.Length; i++)
                    {


                        if (singleRow.Contains(wordAray[i]) && !singleRow.Contains("import"))
                        {
                            keywordCount++;
                        }
                    }
                }
                foreach (string singleRow in line.Split('\n'))
                {
                    System.Diagnostics.Debug.WriteLine("========================================================================== \n");
                    int deletedCount = 0;
                    if (!singleRow.Contains("import"))
                    {

                        string str = singleRow;
                        var reg = new Regex("\".*?\"");
                        var matches = reg.Matches(str);
                        foreach (var item in matches)
                        {
                           
                            string s = item.ToString().Replace("\"", "");
                           

                            string[] operatorsHodingsReover = s.ToLower().Trim().Split(new char[] { ' ', ')', '(', '{', '}', ':', '\r', '\n', ';', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'w', 'u', 'v', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

                            for (int i = 0; i < operatorsHodingsReover.Length; i++)
                            {
                                //System.Diagnostics.Debug.WriteLine("line no" + singleRow + ": Operators Holdings " + operatorsHodings[i]);

                                for (int j = 0; j < operatorAray.Length; j++)
                                {
                                    if (operatorsHodingsReover[i] == operatorAray[j])
                                    {
                                       deletedCount++;
                                         
                                        System.Diagnostics.Debug.WriteLine("Matched: "  + operatorsHodingsReover[i] + ", Line: " + line + "deletedCount: " + deletedCount + "\n");
                                        operatorCount--;
                                    }
                                }

                            }
                        }

                        string[] operatorsHodings = singleRow.ToLower().Trim().Split(new char[] { ' ', '"', ')', '(', '{', '}', ':', '\r', '\n', ';', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'w', 'u', 'v', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries


                        for(int i = 0; i < operatorsHodings.Length; i++)
                        {
                            //System.Diagnostics.Debug.WriteLine("line no" + singleRow + ": Operators Holdings " + operatorsHodings[i]);

                            for (int j = 0; j < operatorAray.Length; j++)
                            {
                                if (operatorsHodings[i] == operatorAray[j])
                                {
                                    System.Diagnostics.Debug.WriteLine("Operators True count " + operatorsHodings[i] + ", Line: " + line+ "\n");
                                   
                                    operatorCount++;
                                }
                            }

                        }
                    }
                    System.Diagnostics.Debug.WriteLine("========================================================================== \n");
                }


                //----------------------------------------------
                foreach (string singleRow in line.Split('\n'))
                {
                    int count = singleRow.Split('"').Length - 1;
                    //System.Diagnostics.Debug.WriteLine("Splited '' count: " + count);

                    if (count % 2 == 0)
                    {
                        stringLiteral = count / 2;
                    }
                }

                // identifiers detector
                foreach (string rowLine in line.Split('\n'))
                {
                    for (int i = 0; i < identifiresArray.Length; i++)
                    {

                        if (rowLine.Contains(identifiresArray[i]) && !rowLine.Contains("import"))
                        {
                            identifires++;
                        }
                    }
                    if (rowLine.Contains("(") && rowLine.Contains(")") && rowLine.Contains(";") && !rowLine.Contains("import"))
                    {
                        identifires++;
                    }

                    if (rowLine.Contains("=") && rowLine.Contains(";") && !rowLine.Contains("import"))
                    {
                        identifires++;
                    }

                    if (rowLine.Contains(".") && rowLine.Contains("(") && rowLine.Contains(")") && rowLine.Contains(";") && !rowLine.Contains("=") && !rowLine.Contains("import"))
                    {
                        identifires++;
                    }
                    if (rowLine.Contains("(") && rowLine.Contains("{") && rowLine.Contains(")") && !rowLine.Contains("=") && !rowLine.Contains(";") && !rowLine.Contains("import"))
                    {
                        identifires++;
                    }
                    if (rowLine.Contains("(") && rowLine.Contains(".") && rowLine.Contains("{") && rowLine.Contains(")") && !rowLine.Contains("=") && !rowLine.Contains(";") && !rowLine.Contains("import"))
                    {
                        identifires++;
                    }

                    if (rowLine.Contains("for"))
                    {
                        identifires = identifires + 3;
                    }


                }// end of identifiers detector


                // numerical values detector
                foreach (string singleRow in line.ToLower().Split(new char[] { '\n', ',', ' ', ')', '(', '{', '}', ':', '\r', ';', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    for (int i = 0; i < ((numericalArray.Length) - 1); i++)
                    {

                        var fResult = string.Join(",", singleRow.ToCharArray());
                        int index = fResult.IndexOf(",");
                        string input = fResult;
                        if (index > 0)
                            input = input.Substring(0, index);

                        if (input.Contains(numericalArray[i]))
                        {
                            numricalCount++;
                        }
                    }

                } // end of numerical values detector



                lineNo++; // increament line number

                //calculate complexity due to size
                cs = (weightKeyword * keywordCount) + (WeightOperators * operatorCount) + (WeightStringLiteral * stringLiteral) + (WeightNumericalVal * numricalCount) + (WeightIdentifers * identifires);
                totalCS = totalCS + cs;
                //System.Diagnostics.Debug.WriteLine("Due to Size: Im in SIze " + cs);
                codeLine = line;
                completeList.Add(new CdueToSize(lineNo, line, keywordCount, operatorCount, numricalCount, identifires, stringLiteral, cs));

                csOuterAccess = cs;

                totalNkw = totalNkw + keywordCount;
                totalNid = totalNid + identifires;
                totalNsl = totalNsl + stringLiteral;
                totalNop = totalNop + operatorCount;
                totalNnv = totalNnv + numricalCount;


                keywordCount = 0;
                operatorCount = 0;
                stringLiteral = 0;
                numricalCount = 0;
                identifires = 0;
                cs = 0;
                CdueToSize c = new CdueToSize(this.totalNkw, this.totalNid, this.totalNop, this.totalNnv, this.totalNsl, this.totalCS);
            }
            finally
            {

            }
        }

        public List<CdueToSize> showData()
        {
            System.Diagnostics.Debug.WriteLine("Am in All Size : " + completeList);
            return completeList;
        }

    }
}


