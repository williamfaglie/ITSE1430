using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib.Web.Mvc.Models
{
    /// <summary>Provides information about a movie.</summary>
    public class MovieModel
    {
        /// <summary>Gets or sets the id.</summary>
        public int Id { get; set; }

        public string Description { get; set; }

        /// <summary>Gets or sets the title.</summary>
        [Required(AllowEmptyStrings = false)]

        public string Title { get; set; }

        //Using auto property here
        [Range(0, Double.MaxValue, ErrorMessage = "Length must be >= 0")]
        public decimal Length { get; set; }
        
        public bool IsOwned { get; set; }
        
    }
}
