using System.ComponentModel.DataAnnotations;

namespace FitLife.Models
{
    public class PasswordsValidation
    {
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string RepeatPassword { get; set; }

    }
}
