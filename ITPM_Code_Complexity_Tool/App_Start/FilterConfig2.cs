using System.Web;
using System.Web.Mvc;

namespace ITPM_Code_Complexity_Tool
{
    public class FilterConfig2
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
