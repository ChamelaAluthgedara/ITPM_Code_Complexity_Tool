using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    public class Controlstructure
    {
        
        public int  LineNO { get; set; }

        public string ProgramStatment { get; set; }

        public int Wtcs { get; set; }

        public int NC { get; set; }

        public int Ccpps { get; set; }

        public int Ccs { get; set; }

    }
}