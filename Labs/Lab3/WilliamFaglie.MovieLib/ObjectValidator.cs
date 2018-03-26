﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilliamFaglie.MovieLib
{
    public static class ObjectValidator
    {
        
            public static IEnumerable<ValidationResult> Validate( this IValidatableObject source )
            {
                var context = new ValidationContext(source);
                var errors = new Collection<ValidationResult>();
                Validator.TryValidateObject(source, context, errors, true);

                return errors;
            }
        }
    
}
