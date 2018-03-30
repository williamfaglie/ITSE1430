//////////////////////////
//Filename: MemoryMovieDatabase.cs
//Author: William Faglie
//Description: This is my MemoryMovieDatabase class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib.Data.Memory
{
    /// <summary>Provides an in-memory movie database.</summary>
    public class MemoryMovieDatabase : MovieDatabase
    {

        protected override Movie AddCore( Movie movie )
        {
            //Clone the object
            movie.Id = _nextId++;
            _movies.Add(Clone(movie));

            // Return the copy
            return movie;
        }

        protected override Movie UpdateCore( Movie movie )
        {
            //Clone the object
            var existing = GetCore(movie.Id);
            Copy(existing, movie);

            //Return a copy
            return movie;
        }

        protected override IEnumerable<Movie> GetAllCore()
        {
            foreach (var movie in _movies)
            {
                if (movie != null)
                    yield return Clone(movie);
            };
        }

        protected override void RemoveCore( int id )
        {
            var existing = GetCore(id);
            if (existing != null)
                _movies.Remove(existing);
        }

        private Movie Clone( Movie item )
        {
            var newMovie = new Movie();
            Copy(newMovie, item);

            return newMovie;
        }

        private void Copy( Movie target, Movie source )
        {
            target.Id = source.Id;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Length = source.Length;
            target.IsOwned = source.IsOwned;
        }

        protected override Movie GetCore( int id )
        {
            foreach (var movie in _movies)
            {
                if (movie.Id == id)
                    return movie;
            };

            return null;
        }

        protected override Movie GetMovieByTitleCore( string title )
        {
            foreach (var movie in _movies)
            {
                if (String.Compare(movie.Title, title, true) == 0)
                    return movie;
            };

            return null;
        }
        private Movie GetById( int id )
        {
            foreach (var movie in _movies)
            {
                if (movie.Id == id)
                    return movie;
            };

            return null;
        }

        private readonly List<Movie> _movies = new List<Movie>();
        private int _nextId = 1;
    }
}
