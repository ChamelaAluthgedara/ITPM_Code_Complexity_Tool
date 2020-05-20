using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
using javax.swing.text;

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
        public int totalCS;

        public  int csOuterAccess;

        public static int weightKeyword;
        public static int WeightIdentifers;
        public static int WeightOperators;
        public static int WeightNumericalVal;
        public static int WeightStringLiteral;

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

        public static string[] identifiresArray = { "class", "return", "main", "System", "out", "print", "printf"};
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
            string[] wordOp = line.Trim().Split(new char[] { ' ', '\r', '\n', ';', '"', '"', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 'w', 'x', 'y', 'z'}, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

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

                //for (int j = 0; j < operatorAray.Length; j++)
                //{

                //    for (int i = 0; i < wordOp.Length; i++)
                //    {
                //        wordOp[i].Remove(wordOp[i].Length - 1).Trim();

                //        if (wordOp[i] == operatorAray[j])
                //        {
                //            operatorCount++;
                //        }

                //    }
                //}
                foreach (string singleRow in line.Split('\n'))
                {
                    if (!singleRow.Contains("import"))
                    {
                    string[] rowArray = singleRow.Trim().Split(new char[] { ' ', ')', '(', '{', '}', ':',  '\r', '\n', ';', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

                    for (int i = 0; i < rowArray.Length; i++)
                    {
                       // System.Diagnostics.Debug.WriteLine("This is row line: " + singleRow);

                        for (int j = 0; j < operatorAray.Length; j++)
                            {
                                rowArray[i].Remove(rowArray[i].Length - 1).Trim();

                                if (rowArray[i] == operatorAray[j])
                                {
                                    operatorCount++;

                                }

                            }
                    }
                    }
                }


                foreach (string singleRow in line.Split('\n', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'))
                {

                    if (singleRow.Contains('"') && singleRow.Contains("(") && singleRow.Contains(")") && singleRow.Contains(";"))
                    {
                        stringLiteral++;
                    }
                }

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


                }

                foreach (string singleRow in line.ToLower().Split(new char[] { '\n', ',', ' ', ')', '(', '{', '}', ':', '\r', ';', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u','v' ,'w', 'x', 'y', 'z' },StringSplitOptions.RemoveEmptyEntries))
                {
                    for (int i = 0; i < ((numericalArray.Length)-1); i++)
                    {
                        //string[] numberArray = singleRow.Trim().Split(new char[] { ' ', ')', '(', '{', '}', ':', '\r', ';', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'w', 'x', 'y', 'z'}, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries


                        //var result = "1234";
                        var fResult = string.Join(",", singleRow.ToCharArray());
                        int index = fResult.IndexOf(",");
                        string input = fResult;
                        if (index > 0)
                            input = input.Substring(0, index);
                       // System.Diagnostics.Debug.WriteLine("This is row line: " + input + ", lenght: " + ((numericalArray.Length) - 1));
                        if (input.Contains(numericalArray[i]))
                        {
                            numricalCount++;
                        }


                  }
                   // System.Diagnostics.Debug.WriteLine("End of for Loop....\n");
                }

                lineNo++;
                cs = (weightKeyword * keywordCount) + (WeightOperators * operatorCount) + (WeightStringLiteral * stringLiteral) + (WeightNumericalVal * numricalCount) + (WeightIdentifers * identifires);
                totalCS = totalCS + cs;
                //System.Diagnostics.Debug.WriteLine("Due to Size: Im in SIze " + cs);
                codeLine = line;
                completeList.Add(new CdueToSize(lineNo, line, keywordCount, operatorCount, numricalCount, identifires, stringLiteral, cs));

                csOuterAccess = cs;

                keywordCount = 0;
                operatorCount = 0;
                stringLiteral = 0;
                numricalCount = 0;
                identifires = 0;
                cs = 0;
                CdueToSize c = new CdueToSize(this.totalCS);

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


