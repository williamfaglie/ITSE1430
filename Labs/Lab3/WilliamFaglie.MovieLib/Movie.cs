//////////////////////////
//Filename: Movie.cs
//Author: William Faglie
//Description: This is my Movie class
//////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib
{
    /// <summary>Provides information about a movie.</summary>
    public class Movie : IValidatableObject 
    {
        /// <summary>Gets or sets the id.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the title.</summary>
        public string Title
        {
            get { return _title ?? ""; }
            set { _title = value; }
        }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }

       
        /// <summary>Getter and setter for length.</summary>
        public decimal Length
        {
            get; set;
        } = 0;

        /// <summary>Getter and setter for checkIsOwned.</summary>
        public bool IsOwned { get; set; }

        /// <summary>Validates the movie.</summary>
        /// <returns>Error message, if any.</returns>
        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
        {
            var errors = new List<ValidationResult>();

            //Title is required
            if (String.IsNullOrEmpty(_title))
                errors.Add(new ValidationResult("Title cannot be empty", new[] { nameof(Title) }));

            //Length >= 0
            if (Length < 0)
                errors.Add(new ValidationResult("Length must be >= 0", new[] { nameof(Length) }));

            return errors;
        }

        private string _title;
        private string _description;
    }
}
