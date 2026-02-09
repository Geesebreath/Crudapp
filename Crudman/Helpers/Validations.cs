using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Crudman.Helpers
{
    public static class Validations
    {
        public static ValidationResult IsStringProperURI(String str)
    {
        if (Uri.IsWellFormedUriString(str, UriKind.Absolute))
        {
#pragma warning disable CS8603 // Possible null reference return.
            return ValidationResult.Success;
#pragma warning restore CS8603 // Possible null reference return.
        }

        return new ValidationResult("String is not a valid URI");
    }
    }
}