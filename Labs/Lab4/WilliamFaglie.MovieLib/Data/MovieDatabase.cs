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
        public Movie Add( Movie movie )
        {
            //Check for null
            movie = movie ?? throw new ArgumentNullException(nameof(movie));

            //Validate movie
            movie.Validate();

            // Verify unique movie
            var existing = GetMovieByTitleCore(movie.Title);
            if (existing != null)
            {
                throw new Exception("Product already exists");
            };

            return AddCore(movie);
        }

        /// <summary>Updates movie if it passes validation.</summary>
        /// <param name="movie"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Movie Update( Movie movie )
        {

            //Check for null
            if (movie == null)
            
                throw new ArgumentNullException(nameof(movie));


            //Validate movie
            movie.Validate();

            //Verify unique movie except current movie
            var existing = GetMovieByTitleCore(movie.Title);
            if (existing != null && existing.Id != movie.Id)
                throw new Exception("Movie already exists");

            //Find existing
            existing = existing ?? GetCore(movie.Id);

            if (existing == null)
                throw new ArgumentException("Movie not found", nameof(movie));

            return UpdateCore(movie);
        }

        /// <summary>Gets all movies in the database.</summary>
        /// <returns></returns>
        public IEnumerable<Movie> GetAll()
        {
            return from m in GetAllCore()
                   orderby m.Title, m.Id descending
                   select m;
        }

        /// <summary>Removes movie based on id.</summary>
        /// <param name="id"></param>
        public void Remove( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");

                RemoveCore(id);
            
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
