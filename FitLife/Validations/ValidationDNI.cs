using FitLife.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FitLife.Validations
{
    public class ValidationDNI: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value is not null)
            {
                return HelperValidation.CheckDNI((string)value);
            }
            else
            {
                return false;
            }
        }
    }
}
