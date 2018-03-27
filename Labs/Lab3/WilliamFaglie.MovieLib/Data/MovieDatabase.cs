using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib.Data
{
    public abstract class MovieDatabase : IMovieDatabase
    {
        //var product = new Product() {
        //    Id = _nextId++,
        //    Name = "iPhone X",
        //    IsDiscontinued = true,
        //    Price = 1500,
        //};
        //_products.Add(product);


        //product = new Product() {
        //    Id = _nextId++,
        //    Name = "Windows Phone",
        //    IsDiscontinued = true,
        //    Price = 15,
        //};
        //_products.Add(product);

        //product = new Product() {
        //    Id = _nextId++,
        //    Name = "Samsung S8",
        //    IsDiscontinued = false,
        //    Price = 800
        //};
        //_products.Add(product);

        public Movie Add( Movie movie, out string message )
        {
            //Check for null
            if (movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate product
            var errors = movie.Validate();
            //var errors = ObjectValidator.Validate(product);
            //if (errors.Count() > 0)
            //{
            //    var error = Enumerable.First(errors);

            //    //Get first error
            //    message = errors.ElementAt(0).ErrorMessage;
            //    return null;
            //};var errors = errors.FirstPrDefault();
            var error = errors.FirstOrDefault();
            if (error != null)
            {
                message = error.ErrorMessage;
                return null;
            };

            // Verify unique product
            var existing = GetMovieByTitleCore(movie.Title);
            if (existing != null)
            {
                message = "Movie already exists";
                return null;
            };

            message = null;
            return AddCore(movie);
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
        public Movie Update( Movie movie, out string message )
        {
            message = "";

            //Check for null
            if (movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate product
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

        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }

        public void Remove( int id )
        {
            if (id > 0)
            {
                RemoveCore(id);
            };
        }




        private readonly List<Movie> _movies = new List<Movie>();
        //private int _nextId = 1;

        protected abstract Movie AddCore( Movie movie );
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract Movie GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Movie UpdateCore( Movie movie );
        protected abstract Movie GetMovieByTitleCore( string title );
    }
}
