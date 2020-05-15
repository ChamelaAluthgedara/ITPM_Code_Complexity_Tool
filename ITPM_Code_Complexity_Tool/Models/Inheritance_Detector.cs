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

        private int INHERITED_NO_CLASS = 0;
        private int INHERITED_ONE_CLASS = 1;
        private int INHERITED_TWO_CLASSES = 2;
        private int INHERITED_THREE_CLASSES = 3;
        private int INHERITED_MORE_THAN_FOUR_CLASSES = 4;


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
            int directWeight = 0;


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
                                    
                               }

                               else
                               {

                                    direct = direct + 1;
                                    
                               }
                           }
                           else if (direct == 0 && WORDS[temp] == "{")
                           {
                               direct = direct + 1;
                               
                           }



                       }
                       /*
                       ci = direct + indirect;
                       this.totalCi = this.totalCi + ci;
                       completeList.Add(new Inheritance(line1, indirect, direct, ci));
                       */




                    }

                }       

              

            }

            
            //exp
            //According to weight set by user
            if (direct == 0)
            {
                directWeight = INHERITED_NO_CLASS;
            }
            else if (direct == 1)
            {
                directWeight = INHERITED_ONE_CLASS;
            }
            else if (direct == 2)
            {
                directWeight = INHERITED_TWO_CLASSES;
            }
            else if (direct == 3)
            {
                directWeight = INHERITED_THREE_CLASSES;
            }
            else if (direct >= 4)
            {
                directWeight = INHERITED_MORE_THAN_FOUR_CLASSES;
            }





            //Calculate Ci value
            ci = directWeight + indirect;
            this.totalDirect = this.totalDirect + directWeight;
            this.totalCi = this.totalCi + ci;
            completeList.Add(new Inheritance(line1, indirect, directWeight, ci));
            
           
        }



            public List<Inheritance> showData()
            {
                completeList.Add(new Inheritance("Total", this.totalIndirect, this.totalDirect, this.totalCi));
                return completeList;
            }

            public void setValOfWeight(int zero, int one, int two, int three, int four)
            {
                this.INHERITED_NO_CLASS = zero;
                this.INHERITED_ONE_CLASS = one;
                this.INHERITED_TWO_CLASSES = two;
                this.INHERITED_THREE_CLASSES = three;
                this.INHERITED_MORE_THAN_FOUR_CLASSES = four;
            }



        }
    }
