using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPM_2020.Models
{
    public class Inheritance
    {

        public String CODELINE;
        public int INDIRECT;
        public int DIRECT;
        public int CI;
      


        public Inheritance(String codeline, int indirect, int direct, int ci)
        {
            this.CODELINE = codeline;
            this.INDIRECT = indirect;
            this.DIRECT = direct;
            this.CI = ci;
            
        }
    }
}