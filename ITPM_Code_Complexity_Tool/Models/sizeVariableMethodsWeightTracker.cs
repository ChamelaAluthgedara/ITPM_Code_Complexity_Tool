using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class sizeVariableMethodsWeightTracker
    {

        public int sizeKeyword { get; set; }

        public int sizeIdentifers { get; set; }

        public int sizeOperators { get; set; }

        public int sizeNumericValues { get; set; }

        public int sizeStringLiteral { get; set; }



        public int variableGlobal { get; set; }

        public int variableLocal { get; set; }

        public int variablePrimitiveDataType { get; set; }

        public int variableCompotiteDataType { get; set; }


        public int methodPrimitiveReturnType { get; set; }

        public int methodCompositeReturnType { get; set; }

        public int methodVoidReturnType { get; set; }

        public int methodPrimitiveDataTypeParameter { get; set; }

        public int methodCompositeDataTypeParameter { get; set; }

    }
}