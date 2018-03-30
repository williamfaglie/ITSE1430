//////////////////////////
//Filename: MovieDatabase.cs
//Author: William Faglie
//Description: This is my MovieDatabase class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib.Data
{
    /// <summary>Provides an in-memory movie database.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Adds movie if it passes validation.</summary>
        /// <param name="movie"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Movie Add( Movie movie, out string message )
        {
            //Check for null
            if (movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate movie
            var errors = movie.Validate();
            var error = errors.FirstOrDefault();
            if (error != null)
            {
                message = error.ErrorMessage;
                return null;
            };

            // Verify unique movie
            var existing = GetMovieByTitleCore(movie.Title);
            if (existing != null)
            {
                message = "Movie already exists";
                return null;
            };

            message = null;
            return AddCore(movie);
        }

        /// <summary>Updates movie if it passes validation.</summary>
        /// <param name="movie"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Movie Update( Movie movie, out string message )
        {
            message = "";

            //Check for null
            if (movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate movie
            var errors = ObjectValidator.Validate(movie);
            if (errors.Count() > 0)
            {
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };

            //Verify unique movie except current movie
            var existing = GetMovieByTitleCore(movie.Title);
            if (existing != null && existing.Id != movie.Id)
            {
                message = "Movie already exists";
                return null;
            };

            //Find existing
            existing = existing ?? GetCore(movie.Id);

            if (existing == null)
            {
                message = "Movie not found.";
                return null;
            };

            return UpdateCore(movie);
        }

        /// <summary>Gets all movies in the database.</summary>
        /// <returns></returns>
        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }

        /// <summary>Removes movie based on id.</summary>
        /// <param name="id"></param>
        public void Remove( int id )
        {
            if (id > 0)
            {
                RemoveCore(id);
            };
        }




        private readonly List<Movie> _movies = new List<Movie>();

        protected abstract Movie AddCore( Movie movie );
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract Movie GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Movie UpdateCore( Movie movie );
        protected abstract Movie GetMovieByTitleCore( string title );
    }
}
