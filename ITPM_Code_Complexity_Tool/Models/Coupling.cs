using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace ITPM_Code_Complexity_Tool.Models
{

    public class Coupling

    {

        private String FILE_NAME;
        public static String[] KEYWORDS = { "extends", "implements", ":" };
        public List<Inheritance> completeList = new List<Inheritance>();
        public int totalDirect = 0;
        public int totalIndirect = 0;
        public int totalCi = 0;


        public void SetFileName(String fileName)
        {
            this.FILE_NAME = fileName;
        }

        public StringBuilder ProcessFile()
        {


            StringBuilder sb = new StringBuilder();

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
                        sb.Append(line + Environment.NewLine + " ");
                        //this.Detect(line);
                    }

                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return sb;

        }
        //Detect Method

        public List<Couplings_model> coupling(String code)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            bool inside_Method = false;
            int inside_Method_count = 0;
            int line_Count = 0;

            code = ProcessFile().ToString();

            String[] m_Store = finding_methods(code).Split('@');
            String m_Name = null;
            List<Couplings_model> list = new List<Couplings_model>();

            int insideClass = 0;

            //Environment.NewLine
            //String[] lines = code.Split(new string[] { "\\r?\\n" }, StringSplitOptions.None);
            String[] lines = code.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (String line in lines)
            {

                count++;
                String[] sample_line_txt = line.Split('\"');

                int regularGlobals = 0;
                int recursiveGlobals = 0;
                int recursive = 0;
                int regularRegulars = 0;
                int regularRecursives = 0;
                int recursiveRegulars = 0;
                int recursiveRecursives = 0;

                for (int i = 1; i <= sample_line_txt.Length; i++)
                {

                    if (i % 2 == 1)
                    {

                        String test = sample_line_txt[i - 1];

                        String str = test.Replace(@"\s+", "_");

                        String rgx = "public|protected|private";


                        var str_reg = Regex.Matches(str, rgx, RegexOptions.IgnoreCase);
                        int str_count = 0;


                        while (str_count < str_reg.Count)
                        {
                            str_count += 1;
                            String txt_line1 = test.Replace(@"\s+", "");
                            String txt_line2 = txt_line1.Replace(")", "@");
                            String txt_line3 = txt_line2.Replace("{", "-.X");
                            String rgx1 = "@-.X";

                            var str1_reg = Regex.Matches(txt_line3, rgx1, RegexOptions.IgnoreCase);
                            int str1_count = 0;

                            while (str1_count < str1_reg.Count)
                            {
                                str1_count += 1;
                                String txt_line4 = test.Replace("(", "@@");
                                String[] sample = txt_line4.Split(new string[] { "@@" }, StringSplitOptions.None);

                                String txt_line5 = sample[0].Replace(@"\s+", "_");
                                String rgx3 = "bool|byte|char|double|float|int|long|short|String|void";
                                //    String rgx3 = "_bool_|_byte_|_char_|_double_|_float_|_int_|_long_|_short_|_String_|_void_";
                                var txt_mt3 = Regex.Match(txt_line5, rgx3, RegexOptions.IgnoreCase);
                                var txt_line5_reg = Regex.Matches(txt_line5, rgx3, RegexOptions.IgnoreCase);
                                int txt_line5_count = 0;
                                while (txt_line5_count < txt_line5_reg.Count)
                                {
                                    txt_line5_count += 1;
                                    String[] sample1 = (txt_line5.Substring(txt_mt3.Index, txt_mt3.Length)).Split('_');
                                    m_Name = sample1[0];
                                    inside_Method = true;
                                }

                            }

                        }

                        if (inside_Method)
                        {
                            String rgx2 = "get|set|add";

                            var test_reg = Regex.Matches(test, rgx2, RegexOptions.IgnoreCase);
                            int test_count = 0;

                            while (test_count < test_reg.Count)
                            {
                                test_count += 1;
                                bool ch = false;
                                String test2 = test.Replace("(", "-.X");
                                String rgx3 = "-.X";

                                var test2_reg = Regex.Matches(test2, rgx3, RegexOptions.IgnoreCase);
                                int test2_count = 0;

                                while (test2_count < test2_reg.Count)
                                {
                                    test2_count += 1;
                                    ch = true;
                                }

                                if (ch)
                                {
                                    regularRegulars++;
                                }

                            }
                        }

                        if (inside_Method)
                        {
                            line_Count++;

                            String txt_line1 = test.Replace("{", "@@");

                            String rgx1 = "@@";

                            var txt_line1_reg = Regex.Matches(txt_line1, rgx1, RegexOptions.IgnoreCase);
                            int txt_line1_count = 0;

                            while (txt_line1_count < txt_line1_reg.Count)
                            {
                                txt_line1_count += 1;
                                inside_Method_count++;
                            }

                            String txt_line2 = test.Replace("}", "@@");

                            String rgx2 = "@@";
                            var txt_line2_reg = Regex.Matches(txt_line2, rgx2, RegexOptions.IgnoreCase);
                            int txt_line2_count = 0;

                            while (txt_line2_count < txt_line2_reg.Count)
                            {
                                txt_line2_count += 1;
                                inside_Method_count--;
                            }

                            if (inside_Method_count == 0)
                            {
                                inside_Method = false;
                                line_Count = 0;
                                m_Name = null;
                                insideClass = 0;
                            }
                            else
                            {
                                for (int k = 0; k < m_Store.Length; k++)
                                {
                                    if (k % 2 == 0)
                                    {
                                        bool check = false;

                                        String txt_line3 = str.Replace("(", "@");
                                        String txt_line4 = txt_line3.Replace(@"\+", "@@");
                                        String txt_line5 = txt_line4.Replace(@"\-", "@@");
                                        String txt_line6 = txt_line5.Replace(@"\*", "@@");
                                        String txt_line7 = txt_line6.Replace(@"\/", "@@");

                                        String rgx3 = "_" + m_Store[k] + "|" + m_Store[k] + "@|@@" + m_Store[k];

                                        var txt_line7_reg = Regex.Matches(txt_line7, rgx3, RegexOptions.IgnoreCase);
                                        int txt_line7_count = 0;

                                        while (txt_line7_count < txt_line7_reg.Count)
                                        {
                                            txt_line7_count += 1;
                                            check = true;
                                        }

                                        if (check)
                                        {
                                            if (m_Store[k + 1] == "Global")
                                            {

                                                for (int m = 0; m < m_Store.Length; m++)
                                                {
                                                    if (m % 2 == 0)
                                                    {
                                                        if (m_Name == m_Store[m])
                                                        {
                                                            if (m_Store[m + 1].Equals("regular"))
                                                            {
                                                                regularGlobals++;
                                                            }
                                                            else
                                                            {
                                                                recursiveGlobals++;
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                if (line_Count > 1)
                                                {
                                                    if (m_Name.Equals(m_Store[k]))
                                                    {
                                                        recursive++;
                                                    }
                                                    else
                                                    {

                                                        for (int m = 0; m < m_Store.Length; m++)
                                                        {
                                                            if (m % 2 == 0)
                                                            {
                                                                if (m_Name.Equals(m_Store[m]))
                                                                {
                                                                    if (m_Store[m + 1].Equals("regular"))
                                                                    {
                                                                        if (m_Store[k + 1].Equals("regular"))
                                                                        {
                                                                            regularRegulars++;
                                                                        }
                                                                        else
                                                                        {
                                                                            regularRecursives++;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (m_Store[k + 1].Equals("regular"))
                                                                        {
                                                                            recursiveRegulars++;
                                                                        }
                                                                        else
                                                                        {
                                                                            recursiveRecursives++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Couplings_model coupling = new Couplings_model();

                coupling.setLine(line);
                coupling.setLineNumber(count);
                coupling.setRecursive(recursive);
                coupling.setRecursive_global(recursiveGlobals);
                coupling.setRecursive_recursive(recursiveRecursives);
                coupling.setRecursive_regular(recursiveRegulars);
                coupling.setRegular_recursive(regularRecursives);
                coupling.setRegular_regular(regularRegulars);
                coupling.setRegular_global(regularGlobals);

                list.Add(coupling);

            }

            return list;

        }

        public String finding_methods(String code)
        {

            int count = 0;
            bool inside_Method = false;
            int inside_Method_count = 0;
            int line_Count = 0;
            bool regular = true;
            String m_Store = "";
            String m_Name = null;

            String[] lines = code.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (String line in lines)
            {
                count++;

                String[] sample_line_txt = line.Split(new string[] { "\"" }, StringSplitOptions.None);
                for (int i = 1; i <= sample_line_txt.Length; i++)
                {

                    if (i % 2 == 1)
                    {

                        String test = sample_line_txt[i - 1];

                        String str = test.Replace("s+", "_");

                        String rgx = "public|protected|private";
                        //  String rgx = "public_|protected_|private_";
                        var str_reg = Regex.Matches(str, rgx, RegexOptions.IgnoreCase);
                        int str_count = 0;

                        while (str_count < str_reg.Count)
                        {
                            str_count += 1;
                            String txt_line1 = test.Replace(@"\s+", "");
                            String txt_line2 = txt_line1.Replace(")", "@");
                            String txt_line3 = txt_line2.Replace("{", "-.X");

                            String rgx1 = "@-.X";
                            var txt_line3_reg = Regex.Matches(txt_line3, rgx1, RegexOptions.IgnoreCase);
                            int txt_line3_count = 0;

                            while (txt_line3_count < txt_line3_reg.Count)
                            {
                                txt_line3_count += 1;
                                String txt_line4 = test.Replace("(", "@@");
                                String[] sample = txt_line4.Split(new string[] { "@@" }, StringSplitOptions.None);

                                String txt_line5 = sample[0].Replace(@"\s+", "_");
                                String rgx3 = "bool|byte|char|double|float|int|long|short|String|void";
                                //  String rgx3 = "_bool_|_byte_|_char_|_double_|_float_|_int_|_long_|_short_|_String_|_void_";
                                var txt_mt3 = Regex.Match(txt_line5, rgx3, RegexOptions.IgnoreCase);

                                var txt_line5_reg = Regex.Matches(txt_line5, rgx3, RegexOptions.IgnoreCase);
                                int txt_line5_count = 0;

                                while (txt_line5_count < txt_line5_reg.Count)
                                {
                                    txt_line5_count += 1;
                                    String[] sample1 = (txt_line5.Substring(txt_mt3.Index, txt_mt3.Length)).Split('_');
                                    m_Name = sample1[0];
                                    inside_Method = true;
                                }

                            }
                        }

                        if (inside_Method)
                        {
                            line_Count++;

                            String txt_line1 = test.Replace("{", "@@");

                            String rgx1 = "@@";
                            var txt_line1_reg = Regex.Matches(txt_line1, rgx1, RegexOptions.IgnoreCase);
                            int txt_line1_count = 0;
                            while (txt_line1_count < txt_line1_reg.Count)
                            {
                                txt_line1_count += 1;
                                inside_Method_count++;
                            }

                            String txt_line2 = test.Replace("}", "@@");

                            String rgx2 = "@@";
                            var txt_line2_reg = Regex.Matches(txt_line2, rgx1, RegexOptions.IgnoreCase);
                            int txt_line2_count = 0;
                            while (txt_line2_count < txt_line2_reg.Count)
                            {
                                txt_line2_count += 1;
                                inside_Method_count--;
                            }

                            if (line_Count > 1)
                            {

                                String txt_line3 = test.Replace(@"\s+", "_").Replace("(", "@@");

                                String rgx3 = "_" + m_Name + "@@|" + m_Name + "_";
                                var txt_line3_reg = Regex.Matches(txt_line3, rgx3, RegexOptions.IgnoreCase);
                                int txt_line3_count = 0;
                                while (txt_line3_count < txt_line3_reg.Count)
                                {
                                    regular = false;
                                }
                            }


                            if (inside_Method_count == 0)
                            {
                                inside_Method = false;
                                line_Count = 0;
                                if (regular)
                                {
                                    if (m_Store == "")
                                    {
                                        m_Store = m_Name + "@regular";
                                    }
                                    else
                                    {
                                        m_Store = m_Store + "@" + m_Name + "@regular";
                                    }
                                }
                                else
                                {
                                    if (m_Store == "")
                                    {
                                        m_Store = m_Name + "@recursive";
                                    }
                                    else
                                    {
                                        m_Store = m_Store + "@" + m_Name + "@recursive";
                                    }
                                }
                                m_Name = null;
                                regular = true;
                            }

                        }
                        else
                        {

                            String rgx1 = "bool|byte|char|double|float|int|long|short|String";

                            var test_reg = Regex.Matches(test, rgx1, RegexOptions.IgnoreCase);
                            int test_count = 0;
                            while (test_count < test_reg.Count)
                            {
                                test_count += 1;
                                String rgx2 = ";";
                                var txt_mt2_reg = Regex.Matches(test, rgx2, RegexOptions.IgnoreCase);
                                int txt_mt2_count = 0;
                                while (txt_mt2_count < txt_mt2_reg.Count)
                                {
                                    txt_mt2_count += 1;
                                    String line_var = test.Substring(0, txt_mt2_reg[txt_mt2_count - 1].Index);
                                    String[] var_arr = line_var.Split(new string[] { "=,;" }, StringSplitOptions.None);
                                    for (int k = 0; k < var_arr.Length; k++)
                                    {
                                        String[] var_arr1 = line_var.Split(',');
                                        for (int m = 0; m < var_arr1.Length; m++)
                                        {
                                            if (m == 0)
                                            {
                                                if (m_Store == "")
                                                {
                                                    m_Store = var_arr1[0] + "@Global";
                                                }
                                                else
                                                {
                                                    m_Store = m_Store + "@" + var_arr1[0] + "@Global";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

            return m_Store;

        }

    }

}
