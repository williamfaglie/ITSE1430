//////////////////////////
//Filename: HomeController.cs
//Author: William Faglie
//Description: This is my HomeController class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WilliamFaglie.MovieLib.Web.Mvc.Controllers
{
    /// <summary>HomeController.</summary>
    public class HomeController : Controller
    {
        /// <summary>Index action result.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}