using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPM_Code_Complexity_Tool.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Table
    {
        public int File_Id { get; set; }
        public string File_Path { get; set; }
        public string File_name { get; set; }
        public string File_Type { get; set; }
    }
}
