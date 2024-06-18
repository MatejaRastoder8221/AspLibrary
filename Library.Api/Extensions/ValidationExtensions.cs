using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Library.Api.Extensions
{
    public static class ValidationExtensions
    {
        public static IEnumerable<object> GetValidationErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.Select(error => new { Property = error.PropertyName, Message = error.ErrorMessage });
        }
    }
}
