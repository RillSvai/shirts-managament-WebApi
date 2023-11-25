using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShirtsManagament.API.Models.Validations.ShirtAttributes
{
    public class GenderAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is char gender) 
            {
                if (Regex.IsMatch(gender.ToString(), @"[m|f]",RegexOptions.IgnoreCase)) 
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult ("Gender of shirt should be one of this values (M,m,F,f).");
            }
            return new ValidationResult("Attribute was applied to wrong type. Expected: 'char'.");
        }
    }
}
