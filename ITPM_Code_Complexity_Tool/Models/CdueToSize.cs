using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class CdueToSize
    {
        public int lineNo;
        public String CODELINE;
        public int keywordCount;
        public int operatorCount;
        public int variableCount;
        public int numricalCount;
        public int identifires;
        public int stringLiteral;
        public int CI;


        public CdueToSize(int lineNo, String codeline, int keywordCount, int operatorCount, int numricalCount, int identifires, int stringLiteral, int cs)
        {
            this.lineNo = lineNo;
            this.CODELINE = codeline;
            this.keywordCount = keywordCount;
            this.operatorCount = operatorCount;
            this.stringLiteral = stringLiteral;
            this.numricalCount = numricalCount;
            this.numricalCount = identifires;
            this.CI = cs;
        }
    }
}