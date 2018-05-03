using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Web.Mvc.Models
{
    /// <summary>Provides information about a product.</summary>
    public class ProductModel
    {
        /// <summary>Gets or sets the id.</summary>
        public int Id { get; set; }

        public string Description { get; set; }

        /// <summary>Gets or sets the name.</summary>
        [Required(AllowEmptyStrings = false)]

        public string Name { get; set; }

        //Using auto property here
        [Range(0, Double.MaxValue, ErrorMessage = "Price must be >= 0")]
        public decimal Price { get; set; }
        
        public bool IsDiscontinued { get; set; }
        
    }
}
