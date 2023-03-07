using FitLife.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace FitLife.Models
{
    public class UsuarioValidation
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El dni es obligatorio.")]
        [ValidationDNI(ErrorMessage = "Formato incorrecto.")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password es obligatoria.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repetir la contraseña es obligatoria.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string PasswordRepeat { get; set; }

        public string Role { get; set; }
    }
}
