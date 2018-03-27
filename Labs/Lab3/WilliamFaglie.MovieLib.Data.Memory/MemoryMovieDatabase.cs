using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib.Data.Memory
{
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

        //private int FindEmptyProductIndex()
        //{
        //    for (var index = 0; index < _products.Length; ++index)
        //    {
        //        if (_products[index] == null)
        //            return index;

        //    };

        //    return -1;
        //}
        protected override Movie UpdateCore( Movie movie )
        {
            //Clone the object
            var existing = GetCore(movie.Id);
            //_products[existingIndex] = Clone(product);
            Copy(existing, movie);

            //Return a copy
            return movie;
        }

        //public IEnumerable<Product> GetAll ()
        //{
        //    //Return a copy so caller 
        //    var items = new List<Product>();

        //    //for (var index = 0; index < _products.Length; ++index)
        //    foreach (var product in _products)
        //    {
        //        if (product != null)
        //            items.Add(Clone(product));
        //    };

        //    return items;
        //}

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
            //for (var index = 0; index < _products.Length; ++index)
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
                //product.Name.CompareTo
                if (String.Compare(movie.Title, title, true) == 0)
                    return movie;
            };

            return null;
        }
        private Movie GetById( int id )
        {
            //for (var index = 0; index < _products.Length; ++index)
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
