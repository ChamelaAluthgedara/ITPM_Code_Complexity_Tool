using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class AllFactors_Processor
    {
        List<AllFactors> completeList = new List<AllFactors>();


        int lineNo = 0;
        int keywordCount = 0;
        int operatorCount = 0;
        int numricalCount = 0;
        int identifires = 0;
        int stringLiteral = 0;
        int cs = 0;
        public int totalCS;

        public int totalCv;
        int WeightDueToVScope = 1;
        int Wpdv = 1;
        int Wcdtv = 1;
        int NoPrimitiveDataTypeVariables = 0;
        int NoCompositeDataTypeVariables = 0;
        int Cv = 0;

        public int Cm;
        public int totalCm;

        public int Wmrt;
        public int Npdtp;
        public int Ncdtp;
        public int Wpdtp = 1;
        public int Wcdtp = 2;

        public static int Wkw = CdueToSize.Wkw;
        public static int Wid = CdueToSize.Wid;
        public static int Wop = CdueToSize.Wop;
        public static int Wnv = CdueToSize.Wnv;
        public static int Wsl = CdueToSize.Wsl;




        // =============================

        int direct = 0;
        int indirect = 0;
        int ci = 0;


        public static String[] KEYWORDS = { "extends", "implements", ":" };
        public int totalDirect = 0;
        public int totalIndirect = 0;
        public int totalCi = 0;


        // =======================================

        private String FILE_NAME;

        public static string[] numericalArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        public static string[] identifiresArray = { "class", "return", "main", "System", "out", "print", "printf" };
        public static string[] wordAray = { "class", "static", "public", "void", "true", "else", "default", "return", "null", "break", "this" };
        public static string[] operatorAray = {  "+", "-", "*", "/", "%", "%.", "++","--",  "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!",
         "|", "^", "~", "<<", ">>", ">>>", "<<<", "->", ".", "::", "+=", "-=", "*=", "/=", " = ", "=", ">>>=", "|=", "&=", "%=", "<<=", ">>=",
         "^=", "."};
        public String[] controlStrucures = { "if", "while", "do", "switch", "for" };


        public static string[] primitiveTypes = { "char", "byte", "short", "int", "long", "boolean", "float", "double" };

        public static string[] primitiveDataTypes = { "char", "byte", "short", "int", "long", "boolean", "float", "double", "String" };

        public static string[] compositeDataTypes = { "ArrayList ", "List", "[" };

        public AllFactors_Processor()  //Constructor
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
            string[] wordOp = line.Trim().Split(new char[] { ' ', '\r', '\n', ',', ';', '"', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 'w', 'x', 'y', 'z' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

            try
            {

                foreach (string singleRow in line.Split('\n'))
                {
                    for (int i = 0; i < wordAray.Length; i++)
                    {

                        System.Diagnostics.Debug.WriteLine("This is row line: " + singleRow);

                        if (singleRow.Contains(wordAray[i]))
                        {
                            keywordCount++;
                        }
                    }
                }

                for (int j = 0; j < operatorAray.Length; j++)
                {

                    for (int i = 0; i < wordOp.Length; i++)
                    {
                        wordOp[i].Remove(wordOp[i].Length - 1).Trim();

                        if (wordOp[i] == operatorAray[j])
                        {
                            operatorCount++;
                        }

                    }
                }


                foreach (string singleRow in line.Split('\n'))
                {
                    System.Diagnostics.Debug.WriteLine("This is row line: " + singleRow);

                    if (singleRow.Contains('"') && singleRow.Contains("(") && singleRow.Contains(")") && singleRow.Contains(";"))
                    {
                        stringLiteral++;
                    }
                }

                foreach (string rowLine in line.Split('\n'))
                {
                    for (int i = 0; i < identifiresArray.Length; i++)
                    {

                        System.Diagnostics.Debug.WriteLine("This is row line: " + rowLine);

                        if (rowLine.Contains(identifiresArray[i]))
                        {
                            identifires++;
                        }

                    }

                    if (rowLine.Contains("for"))
                    {
                        identifires = identifires + 3;
                    }


                }

                foreach (string singleRow in line.Split('\n'))
                {
                    for (int i = 0; i < numericalArray.Length; i++)
                    {

                        System.Diagnostics.Debug.WriteLine("This is row line: " + singleRow);

                        if (singleRow.Contains(numericalArray[i]))
                        {
                            numricalCount++;
                        }
                    }
                }


                foreach (string rowLine in line.Split('\n'))
                {
                    System.Diagnostics.Debug.WriteLine("Line code " + rowLine);

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


                // =====================================================================================================

                // Inheritance Calculator


                String[] KEYWORDS = { "extends", "implements", ":" };
                string[] WORDS = line.Split(' ');
                //Check if this line contains keywords

                for (int position = 0; position < WORDS.Length; position++)
                {
                    foreach (String keyword in KEYWORDS)//Checking for keywords
                    {
                        if (WORDS[position] == keyword)//A Keyword on the line is found
                        {

                            for (int temp = position + 1; temp < WORDS.Length; temp++)//Checking words after keywords
                            {
                                if (WORDS[temp] == ",")
                                {
                                    if (direct == 0)
                                    {
                                        direct = direct + 2;//One defined Class found
                                        this.totalDirect = this.totalDirect + direct;
                                    }

                                    else
                                    {
                                        direct = direct + 1;
                                        this.totalDirect = this.totalDirect + direct;
                                    }
                                }
                                else if (direct == 0 && WORDS[temp] == "{")
                                {
                                    direct = direct + 1;
                                    this.totalDirect = this.totalDirect + direct;
                                }

                            }
                            ci = direct + indirect;
                            this.totalCi = this.totalCi + ci;
                            this.totalCi = 0;
                        }
                    }
                    //Calculate Ci value
                    if (ci == 0)
                    {
                        this.totalCi = 0;
                    }

                }
                

                // end of Inheritance calculator

                // =============================================================================================================

























                // ======================================================================================================




                lineNo++;
                cs = (Wkw * keywordCount) + (Wid * operatorCount) + (Wop * stringLiteral) + (Wnv * numricalCount) + (Wsl * identifires);
                totalCS = totalCS + cs;

                Cv = (WeightDueToVScope * ((Wpdv * NoPrimitiveDataTypeVariables) + (Wcdtv * NoCompositeDataTypeVariables)));
                totalCv = totalCv + Cv;

                Cm = Wmrt + (Wpdtp * Npdtp) + (Wcdtp * Ncdtp);
                totalCm = totalCm + Cm;

                completeList.Add(new AllFactors(lineNo, line, cs, Cv, Cm, totalCi));

                System.Diagnostics.Debug.WriteLine("Process data: " + cs +" , "+ Cv + " , " +  Cm);



                Npdtp = 0;
                Ncdtp = 0;
                Cm = 0;
                keywordCount = 0;
                operatorCount = 0;
                stringLiteral = 0;
                numricalCount = 0;
                identifires = 0;
                cs = 0;
                NoPrimitiveDataTypeVariables = 0;
                NoCompositeDataTypeVariables = 0;
                Cv = 0;

                

            }
            catch(Exception)
            {

            }
        }

        public List<AllFactors> showData()
        {
            return completeList;
        }

    }
}