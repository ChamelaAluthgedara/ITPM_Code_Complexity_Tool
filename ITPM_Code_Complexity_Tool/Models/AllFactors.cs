using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class AllFactors
    {
        public int lineNo;
        public String CODELINE;
        public int CS;
        public int CV;
        public int CM;

        public int Ci;
        public int Ccp;
        public int Ccs;
        public int TCps;




        public AllFactors(int lineNo, String codeline, int cs, int cv, int cm, int ci)
        {
            this.lineNo = lineNo;
            this.CODELINE = codeline;
            this.CS = cs;
            this.CV = cv;
            this.CM = cm;
            this.Ci = ci;
        }

    }

    //public class AllFactorsVariables
    //{
    //    public int lineNo;
    //    public String CODELINE;
        



    //    public AllFactorsVariables(int lineNo, String codeline, int cv)
    //    {
    //        this.lineNo = lineNo;
    //        this.CODELINE = codeline;
            
    //    }

    //}

    //public class AllFactorsMethods
    //{
    //    public int lineNo;
    //    public String CODELINE;
    //    public int CM;



    //    public AllFactorsMethods(int lineNo, String codeline, int cm)
    //    {
    //        this.lineNo = lineNo;
    //        this.CODELINE = codeline;
    //        this.CM = cm;
    //    }

    //}
}