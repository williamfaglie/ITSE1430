﻿/*
 * ITSE 1430
 * Sample implementation
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MovieLib.Web.Models
{
    /// <summary>Provides a view model for movies.</summary>
    public class MovieViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Display(Name = "Is Owned")]
        public bool IsOwned { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Length { get; set; }

        public Rating Rating { get; set; }

        //CR3 William Faglie - Used auto property 'Range' to make sure Release year is properly limited.
        [Range(1900, 2100, ErrorMessage = "Release Year must be between 1900 and 2100")]
        public int ReleaseYear { get; set; }

        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext ) => Enumerable.Empty<ValidationResult>();        
    }
}