using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib
{
    /// <summary>Provides information about a product.</summary>
    public class Movie : IValidatableObject
    {
        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }

        /// <summary>Gets or sets the name.</summary>
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value; }
        }

        /// <summary>Getter and setter for length.</summary>
        public decimal Price
        {
            get; set;
        } = 0;

        /// <summary>Getter and setter for checkIsOwned.</summary>
        public bool IsDiscontinued { get; set; }

        /// <summary>Validates the product.</summary>
        /// <returns>Error message, if any.</returns>
        public string Validate ()
        {
            //Name is required
            if (String.IsNullOrEmpty(_name))
                return "Name cannot be empty";

            //Price >= 0
            if (Price < 0)
                return "Price must be >= 0";

            return "";
        }

        /// <summary>Name of the product.</summary>
        private string _name;
        /// <summary>Description of the product.</summary>
        private string _description;
    }
}
