using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class AllFactors
    {
        // total calculations for size, variabl, methods factor
        public int lineNoAllF;
        public String CODELINEallF;
        public int CSAllF;
        public int CVAllF;
        public int CMAllF;

        // total calculations for inherirance
        public int CiAllF;
        public int CcpAllF;
        public int CcsAllF;
        public int TCpsAllF;

        // total calculations for control Structure
        public int totalCsColumn;
        public int totalCvColumn;
        public int totalCmColumn;
        public int totalCiColumn;
        public int totalCtsColumn;
        public int totalTCpsAllFColumn;


        // total column values
        public AllFactors(int lineNo, String codeline, int cs, int cv, int cm, int ci, int cts, int tcps)
        {
            this.lineNoAllF = lineNo;
            this.CODELINEallF = codeline;
            this.CSAllF = cs;
            this.CVAllF = cv;
            this.CMAllF = cm;
            this.CiAllF = ci;
            this.CcsAllF = cts;
            this.TCpsAllF = tcps;
        }

        // total bottom row (single total columns values)
        public AllFactors(int totalCsColumn, int totalCvColumn, int totalCmColumn, int totalCiColumn, int totalCtsColumn, int totalTCpsAllFColumn)
        {
            this.totalCsColumn = totalCsColumn;
            this.totalCvColumn = totalCvColumn;
            this.totalCmColumn = totalCmColumn;
            this.totalCiColumn = totalCiColumn;
            this.totalCtsColumn = totalCtsColumn;
            this.totalTCpsAllFColumn = totalTCpsAllFColumn;
        }


    }


}