//////////////////////////
//Filename: RouteConfig.cs
//Author: William Faglie
//Description: This is my RouteConfig class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nile.Web.Mvc
{
    /// <summary>Route config.</summary>
    public class RouteConfig
    {
        /// <summary>Static RegisterRoutes method.</summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
