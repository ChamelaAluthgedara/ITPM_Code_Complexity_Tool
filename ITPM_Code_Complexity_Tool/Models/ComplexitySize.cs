﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class ComplexitySize
    {
        int lineNo = 0;
        int keywordCount = 0;
        int operatorCount = 0;
        int variableCount = 0;
        int numricalCount = 0;
        int identifires = 0;
        int stringLiteral = 0;
        int cs = 0;

        public static string rootFolder = "C:\\Users\\Chamela Aluthgedara\\Desktop\\ITPM_2020_1\\ITPM_2020\\ITPM_2020\\uploadedFiles\\";
        private String FILE_NAME;

        public static string[] numericalArray =
        {
            "+1", "+2", "+3", "+4", "+5", "+6", "+7", "+8", "+9" ,"-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9" ,"1", "2", "3", "4", "5", "6", "7", "8", "9" , "0"
        };
        public static string[] identifiresArray = {

            "int ",
            "float ",
            "double ",
            "char ",
            "string ",
            "string[] ",
            "long ",
            "Boolean ",

             "int",
            "float",
            "char",
            "double",
            "string",
            "string[]",
            "long",
            "Boolean",

            "int float ",
            "public float ",
            "public double ",
            "public string ",
            "public long ",
            "public Boolean ",

            "private int ",
            "private float ",
            "private double",
            "private string",
            "private long",
            "private Boolean",

            "protected int ",
            "protected float ",
            "protected double ",
            "protected string ",
            "protected long ",
            "protected Boolean ",

            "public static int ",
            "public static float ",
            "public static double ",
            "public static string ",
            "public static long ",
            "public static Boolean ",

            "private static int ",
            "private static float ",
            "private static double ",
            "private static string ",
            "private static long ",
            "private static Boolean ",

            "protected static int ",
            "protected static float ",
            "protected static double ",
            "protected static string ",
            "protected static long ",
            "protected static Boolean ",

        };

        public static string[] stringLiteralArray = { "System.out.printf(", "System.out.printf( ", "System.out.print( ", "System.out.println( ", "System.out.print(", "System.out.println(" };
        public static string[] wordAray = { "class", "static", "public", "void", "true", "else", "default", "return", "null", "break", "this" };
        public static string[] operatorAray = {  "+", "-", "*", "/", "%", "%.", "++","--",  "==", "!=", ">", "<", ">=", "<=", "&&", "||", "!",
         "|", "^", "~", "<<", ">>", ">>>", "<<<", "->", ".", "::", "+=", "-=", "*=", "/=", " = ", "=", ">>>=", "|=", "&=", "%=", "<<=", ">>=",
         "^="  };


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

            this.FILE_NAME = "lol.java";
            Console.WriteLine("TikTok");

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
                        this.GetKeywordCount(line);
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
            string[] wordNm = line.Split(new char[] { '(', ')', '\r', '\n', ',', ';', '=', ' ' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

            string[] wordSl = line.Split(new char[] { ' ', '"', '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

            string[] wordOp = line.Split(new char[] { ' ', '\r', '\n', ',', ';', '"', '.' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries

            string[] words = line.Split(new char[] { ' ', '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries
            try
            {
                for (int j = 0; j < wordAray.Length; j++)
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i] == wordAray[j])
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
                for (int j = 0; j < stringLiteralArray.Length; j++)
                {
                    for (int i = 0; i < wordSl.Length; i++)
                    {
                        if (wordSl[i] == stringLiteralArray[j])
                        {
                            stringLiteral++;

                        }
                    }
                }

                for (int j = 0; j < numericalArray.Length; j++)
                {
                    for (int i = 0; i < wordNm.Length; i++)
                    {
                        if (wordNm[i] == numericalArray[j])
                        {
                            numricalCount++;

                        }
                        if (" " + wordNm[i] + ";" == numericalArray[j])
                        {
                            numricalCount++;

                        }
                        if (wordNm[i] == j + numericalArray[j])
                        {
                            numricalCount++;

                        }
                        if ("= " + wordNm[i] == numericalArray[j])
                        {
                            numricalCount++;

                        }
                    }
                }
                lineNo++;
                cs = keywordCount + operatorCount + stringLiteral + numricalCount + identifires;
                completeList.Add(new CdueToSize(lineNo, line, keywordCount, operatorCount, numricalCount, identifires, stringLiteral, cs));
                keywordCount = 0;
                operatorCount = 0;
                stringLiteral = 0;
                numricalCount = 0;
                identifires = 0;
                cs = 0;


            }
            finally
            {

            }
        }

        public List<CdueToSize> showData()
        {
            return completeList;
        }

    }
}


