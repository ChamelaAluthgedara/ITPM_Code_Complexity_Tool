using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class Inheritance_Detector
    {


        private String FILE_NAME;
        public static String[] KEYWORDS = { "extends", "implements", ":" };
        public List<Inheritance> completeList = new List<Inheritance>();
        public int totalDirect = 0;
        public int totalIndirect = 0;
        public int totalCi = 0;

        public Inheritance_Detector()  //Constructor
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

                        this.Detect(line);
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
        //Detect Method

        public void Detect(string line1)
        {


            int direct = 0;
            int indirect = 0;
            int ci = 0;

            String[] KEYWORDS = { "extends", "implements", ":" };

            string[] WORDS = line1.Split(' ');


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
                        completeList.Add(new Inheritance(line1, indirect, direct, ci));

                    }



                }
                //Calculate Ci value


            }

            if (ci == 0)
            {
                completeList.Add(new Inheritance(line1, indirect, direct, ci));
            }
        }


        public List<Inheritance> showData()
        {
            completeList.Add(new Inheritance("Total", this.totalIndirect, this.totalDirect, this.totalCi));
            return completeList;
        }



    }
}