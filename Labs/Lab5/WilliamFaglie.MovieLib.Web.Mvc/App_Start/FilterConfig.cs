//////////////////////////
//Filename: FilterConfig.cs
//Author: William Faglie
//Description: This is my FilterConfig class
//////////////////////////
using System.Web;
using System.Web.Mvc;

namespace Nile.Web.Mvc
{
    /// <summary>Filters.</summary>
    public class FilterConfig
    {
        /// <summary>static RegisterGlobalFilters method.</summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters( GlobalFilterCollection filters )
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
