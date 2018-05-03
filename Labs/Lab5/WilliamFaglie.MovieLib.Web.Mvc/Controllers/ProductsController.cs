//////////////////////////
//Filename: ProductsController.cs
//Author: William Faglie
//Description: This is my ProductsController class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WilliamFaglie.MovieLib.Data;
using WilliamFaglie.MovieLib.Data.Sql;
using WilliamFaglie.MovieLib.Web.Mvc.Models;

namespace WilliamFaglie.MovieLib.Web.Mvc.Controllers
{
    /// <summary>Products controller.</summary>
    public class ProductsController : Controller
    {
        /// <summary>Contructor. ConnectionString to database.</summary>
        public ProductsController()
        {
            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];
            _database = new SqlMovieDatabase(connString.ConnectionString);
        }
        private readonly IMovieDatabase _database;

        /// <summary>List action result.</summary>
        /// <returns></returns>
        [HttpGet]   
        public ActionResult List()
        {
            var movies = _database.GetAll();

            return View(movies.Select(m => m.ToModel()));
        }

        /// <summary>Create action result. Get.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View(new MovieModel());
        }

        /// <summary>Create action result. Post.</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create ( MovieModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var movie = model.ToDomain();

                    movie = _database.Add(movie);

                    return RedirectToAction(nameof(List));
                };
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message); 
            };

            return View(model);
        }

        /// <summary>Edit action result. Get.</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit ( int id)
        {
            var movie = _database.GetAll().FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie.ToModel());
        }

        /// <summary>Edit action result. Post.</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit( MovieModel model )
        {

            try
            {
                if (ModelState.IsValid)
                { 
                    var movie = model.ToDomain();

                    movie = _database.Update(movie);

                    return RedirectToAction("List");
                };
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };

            return View(model);
        }

        /// <summary>Delete action result. Get.</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("movies/delete/{id}")]
        public ActionResult Delete( int id )
        {
            var movie = _database.GetAll().FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie.ToModel());
        }

        /// <summary>Delete action result. Post.</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete( MovieModel model )
        {
            try
            {
                var movie = _database.GetAll().FirstOrDefault(m => m.Id == model.Id);

                if (movie == null)
                    return HttpNotFound();

                _database.Remove(model.Id);

                return RedirectToAction(nameof(List));
            } catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };

            return View(model);
        }
    }
}