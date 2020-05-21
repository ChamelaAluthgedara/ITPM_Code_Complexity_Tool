﻿using System;
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

        public int totalNkw;
        public int totalNid;
        public int totalNop;
        public int totalNnv;
        public int totalNsl;
        public int totalCSCal;



        public static int Wkw = 1;
        public static int Wid = 1;
        public static int Wop = 1;
        public static int Wnv = 1;
        public static int Wsl = 1;


        public CdueToSize(int lineNo, String codeline, int keywordCount, int operatorCount, int numricalCount, int identifires, int stringLiteral, int cs)
        {
            this.lineNo = lineNo;
            this.CODELINE = codeline;
            this.keywordCount = keywordCount;
            this.operatorCount = operatorCount;
            this.stringLiteral = stringLiteral;
            this.numricalCount = numricalCount;
            this.identifires = identifires;
            this.CI = cs;
        }


        public CdueToSize(int totalNkw, int totalNid, int totalNop, int totalNnv, int totalNsl, int totalCS)
        {
            this.totalNkw = totalNkw;
            this.totalNid = totalNid;
            this.totalNop = totalNop;
            this.totalNnv = totalNnv;
            this.totalNsl = totalNsl;
            this.totalCSCal = totalCS;
        }
        public CdueToSize()
        {

        }

    }

    public class CdueToVariables
    {
        public int lineNo;
        public String CODELINE;
        public int WeightDueToVScope;
        public int NoPrimitiveDataTypeVariables;
        public int NoCompositeDataTypeVariables;
        public int Cv;

        public int totalVCCal;
        public int totalWeightDueToVScope;
        public int totalNoPrimitiveDataTypeVariables;
        public int totalNoCompositeDataTypeVariables;

        public static int WeightGlobalVariable = 1;
        public static int WeightLocalVariable = 1;
        public static int WeightPrimitiveDataTypeVariable = 1;
        public static int WeightCompositeDataTypeVariable = 1;

        public CdueToVariables()
        {

        }
        public CdueToVariables(int lineNo, String codeline, int WeightDueToVScope, int NoPrimitiveDataTypeVariables, int NoCompositeDataTypeVariables, int cv)
        {
            this.lineNo = lineNo;
            this.CODELINE = codeline;
            this.WeightDueToVScope = WeightDueToVScope;
            this.NoPrimitiveDataTypeVariables = NoPrimitiveDataTypeVariables;
            this.NoCompositeDataTypeVariables = NoCompositeDataTypeVariables;
            this.Cv = cv;
        }
        public CdueToVariables(int totalVCCal, int totalWeightDueToVScope, int totalNoPrimitiveDataTypeVariables, int totalNoCompositeDataTypeVariables)
        {
            this.totalWeightDueToVScope = totalWeightDueToVScope;
            this.totalNoPrimitiveDataTypeVariables = totalNoPrimitiveDataTypeVariables;
            this.totalNoCompositeDataTypeVariables = totalNoCompositeDataTypeVariables;
            this.totalVCCal = totalVCCal;
        }

    }

    public class CdueToMethod
    {
        public int lineNo;
        public String CODELINE;
        public int NumberOfCompositeDataTypeParameters;
        public int NumberOfPrimitiveDataTypeParameters;
        public int methodReturnType;
        public int Cm;

        public int totalCMCal;
        public int totalNcdtp;
        public int totalNpdtp;
        public int totalWmrt;


        public Boolean voidDetected = false;


        public static int Wprimitivedtp = 1;
        public static int Wcompositedtp = 2;
        public static int Wvoidtype = 0;
        public static int WprimitiveDatatype = 1;
        public static int WcompositeParameter = 2;


        public CdueToMethod(int lineNo, String codeline, int NumberOfCompositeDataTypeParameters, int NoPrimitiveDataTypeVariables, int methodReturnType, Boolean voidDetec, int Cm)
        {
            this.lineNo = lineNo;
            this.CODELINE = codeline;
            this.NumberOfCompositeDataTypeParameters = NumberOfCompositeDataTypeParameters;
            this.NumberOfPrimitiveDataTypeParameters = NoPrimitiveDataTypeVariables;
            this.methodReturnType = methodReturnType;
            this.voidDetected = voidDetec;
            this.Cm = Cm;
        }
        public CdueToMethod(int totalCMCal, int totalNcdtp, int totalNpdtp, int totalWmrt)
        {
            this.totalNcdtp = totalNcdtp;
            this.totalNpdtp = totalNpdtp;
            this.totalWmrt = totalWmrt;
            this.totalCMCal = totalCMCal;
        }
        public CdueToMethod()
        {
        }

    }
}