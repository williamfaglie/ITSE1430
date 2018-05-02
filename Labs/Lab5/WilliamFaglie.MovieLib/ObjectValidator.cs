//////////////////////////
//Filename: ObjectValidator.cs
//Author: William Faglie
//Description: This is my ObjectValidator class
//////////////////////////
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib
{
    /// <summary>Enforces Validation.</summary>
    public static class ObjectValidator
    {
        /// <summary>Enumerates through database to determine any errors.</summary>
        /// <param name="source"></param>
        /// <returns></returns>
            public static IEnumerable<ValidationResult> TryValidate( this IValidatableObject source )
            {
                var context = new ValidationContext(source);
                var errors = new Collection<ValidationResult>();
                Validator.TryValidateObject(source, context, errors, true);

                return errors;
            }

        /// <summary>Determines validation using the validation context.</summary>
        /// <param name="source"></param>
        public static void Validate ( this IValidatableObject source)
        {
            var context = new ValidationContext(source);
            Validator.ValidateObject(source, context, true);
        }
        }
    
}
