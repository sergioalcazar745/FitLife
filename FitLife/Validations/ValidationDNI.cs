using FitLife.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FitLife.Validations
{
    public class ValidationDNI: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return HelperValidationDNI.CheckDNI((string)value);
        }
    }
}
