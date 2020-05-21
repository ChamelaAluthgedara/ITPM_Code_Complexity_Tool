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


        // size factor weights
        private static int weightKeyword = 1;
        private static int WeightIdentifers = 1;
        private static int WeightOperators = 1;
        private static int WeightNumericalVal = 1;
        private static int WeightStringLiteral = 1;

        // variable factor weights
        private static int globalVariable = 2;
        private static int localVariable = 1;
        private static int primitiveDataTypeVariable = 1;
        private static int CompositeDataTypeVariable = 2;

        // methods factor weights
        private static int methodPeReturnType = 1;
        private static int methodCReturnType = 2;
        private static int methodVoid = 0;
        private static int methodPDataTypeParameter = 1;
        private static int methodCTypeParameter = 2;

        // methods factor control structure
        private static int ifElseIfWeight = 1;
        private static int forWileDoWhileWeight = 1;
        private static int SwitchWeight = 1;
        private static int CaseWeight = 1;


        int lineNo = 0;
        string codeLine;
        private int CS = 0;
        private int CV = 0;
        private int CM = 0;
        private int CI = 0;
        private int Cts = 0;

        private int Totalcps;

        public int totalCsColumn;
        public int totalCvColumn;
        public int totalCmColumn;
        public int totalCiColumn;
        public int totalCcpColumn;
        public int totalCtsColumn;
        public int totalTCpsAllFColumn;

        private String FILE_NAME;

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


        ComplexitySize size = new ComplexitySize();
        ComplexityVariables variables = new ComplexityVariables();
        ComplexityMethods methods = new ComplexityMethods();
        Inheritance_Detector inheritance = new Inheritance_Detector();
        ControlStructureDetector controlStructure = new ControlStructureDetector();


        ControlStructureWeight Weight = new ControlStructureWeight()
        {
            ifElseIfWeight = ifElseIfWeight,
            forWileDoWhileWeight = forWileDoWhileWeight,
            SwitchWeight = SwitchWeight,
            CaseWeight = CaseWeight
        };



        public void GetKeywordCount(string line)
        {
            try
            {

                foreach (string singleRow in line.Split('\n'))
                {
                    size.GetKeywordCount(line);
                    variables.GetVariablesCount(line);
                    methods.GetMethodCount(line);
                    inheritance.Detect(line);
                    controlStructure.ControStructureDetect(line, Weight);

                    this.lineNo++;
                    this.codeLine = size.codeLine;

                    this.CS = size.csOuterAccess;
                    this.CV = variables.CvouterAccess;
                    this.CM = methods.CmouterAccess;
                    this.CI = inheritance.CiouterAccess;
                    this.Cts = controlStructure.CtsouterAccess;


                    size.getWeight(weightKeyword, WeightIdentifers, WeightOperators, WeightNumericalVal, WeightStringLiteral);
                    variables.getWeight(globalVariable, localVariable, primitiveDataTypeVariable, CompositeDataTypeVariable);
                    methods.getWeight(methodPeReturnType, methodCReturnType, methodVoid, methodPDataTypeParameter, methodCTypeParameter);



                    System.Diagnostics.Debug.WriteLine("Variables in AllFactors : " + this.CV);

                    Totalcps = this.CS + this.CV + this.CM + this.CI + this.Cts;

                    totalCsColumn = totalCsColumn + this.CS;
                    totalCvColumn = totalCvColumn + this.CV;
                    totalCmColumn = totalCmColumn + this.CM;
                    totalCiColumn = totalCiColumn + this.CI;
                    totalCtsColumn = totalCtsColumn + this.Cts;
                    totalTCpsAllFColumn = totalTCpsAllFColumn + Totalcps;

                    System.Diagnostics.Debug.WriteLine("total column Cs : " + totalCsColumn);

                    completeList.Add(new AllFactors(this.lineNo, this.codeLine, this.CS, this.CV, this.CM, this.CI, this.Cts, this.Totalcps));
                    AllFactors allFac = new AllFactors(this.totalCsColumn, this.totalCvColumn, this.totalCmColumn, this.totalCiColumn, this.totalCtsColumn, this.totalTCpsAllFColumn);
                    CS = 0;
                    CV = 0;
                    CM = 0;
                    CI = 0;
                    Cts = 0;
                    Totalcps = 0;


                }
            }
            catch (Exception e)
            {

            }
        }

        public List<AllFactors> showData()
        {

            // System.Diagnostics.Debug.WriteLine("Am in All factors : " + completeList);
            return completeList;
        }
    }
}