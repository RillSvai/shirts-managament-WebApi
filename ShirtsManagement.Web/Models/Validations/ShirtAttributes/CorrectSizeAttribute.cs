﻿using System.ComponentModel.DataAnnotations;

namespace ShirtsManagement.Web.Models.Validations.ShirtAttributes
{
    public class CorrectSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is not Shirt shirt)
                return new ValidationResult("Attribute was applied to wrong type. Expected: 'Shirt'.");
            double minSize = shirt.Gender switch 
            {
                'm' or 'M' => 8,
                _ => 6,
            };
            if (shirt.Size < minSize) 
            {
                return new ValidationResult($"Size of shirt should be greater or equal {minSize}.");
            }
            return ValidationResult.Success;
        }
    }
}
