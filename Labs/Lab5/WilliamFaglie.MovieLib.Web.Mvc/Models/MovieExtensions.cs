//////////////////////////
//Filename: MovieExtensions.cs
//Author: William Faglie
//Description: This is my MovieExtensions class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WilliamFaglie.MovieLib.Web.Mvc.Models
{
    /// <summary>MovieExtensions static class.</summary>
    public static class MovieExtensions
    {
        /// <summary>ToModel static method.</summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static MovieModel ToModel ( this Movie source )
            => new MovieModel() {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                IsOwned = source.IsOwned,
                Length = source.Length
            };

        /// <summary>ToDomain static method.</summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Movie ToDomain ( this MovieModel source )
            => new Movie() {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                IsOwned = source.IsOwned,
                Length = source.Length
            };
    }
}