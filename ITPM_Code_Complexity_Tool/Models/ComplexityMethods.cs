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

        public int Wmrt;
        public int Npdtp;
        public int Ncdtp;

        public int Wpdtp = 1;
        public int Wcdtp = 2;


        public static string rootFolder = "../uploadedFiles";
        private String FILE_NAME;

        List<CdueToMethod> completeList = new List<CdueToMethod>();

        public ComplexityMethods()  //Constructor
        {

        }

        public void SetFileName(String fileName)
        {
            this.FILE_NAME = fileName;
        }

        public static string[] primitiveTypes = { 
            "public char",
            "public byte",
            "public short",
            "public int",
            "public long",
            "public boolean",
            "public float",
            "public double",

            "public static char",
            "public static byte",
            "public static short",
            "public static int",
            "public static long",
            "public static boolean",
            "public static float",
            "public static double",
        };

        public static string[] compositeTypes = {

            "public static void main",
            " public static void main(String[] args) ",
            "public static void main(String[] args) ",
            " public static void main(String[] args)",
            "public static void main(",
            "public static void main ",
            " public static void main "
        };

        public void ProcessFile()
        {

            this.FILE_NAME = "userUploadFile.java";

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
                string[] words = line.Trim().Split(new char[] { '\n', '{', '(', ')', '}', ']', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries); //Split by words and remove new lines empty entries
                try
                {
                    for (int j = 0; j < compositeTypes.Length; j++)
                    {
                        for (int i = 0; i < words.Length; i++)
                        {
                            string wordLine = words[i];
                            string arraWord = compositeTypes[j];
                            if (arraWord.Equals(wordLine))
                            {
                                System.Diagnostics.Debug.WriteLine("line: " + words[i]);
                                Ncdtp++;
                            }
                        }
                    }
                }
                finally
                {

                }

                try
                {
                    for (int j = 0; j < primitiveTypes.Length; j++)
                    {
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i] == primitiveTypes[j])
                            {
                                Npdtp++;
                            }
                        }
                    }
                }

                finally
                {

                }
                System.Diagnostics.Debug.WriteLine("composite: " + Ncdtp);
                lineNo++;
                Cm = Wmrt + (Wpdtp * Npdtp) + (Wcdtp * Ncdtp);
                completeList.Add(new CdueToMethod(lineNo, line, Ncdtp, Npdtp, Wmrt, Cm));
                
                
                Npdtp = 0;
                Ncdtp = 0;
                Cm = 0;
            }
            finally
            {

            }
        }

        public List<CdueToMethod> showData()
        {
            return completeList;
        }

    }
}


