using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WilliamFaglie.MovieLib.Web.Mvc.Models
{
    public static class MovieExtensions
    {
        public static MovieModel ToModel ( this Movie source )
            => new MovieModel() {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                IsOwned = source.IsOwned,
                Length = source.Length
            };

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