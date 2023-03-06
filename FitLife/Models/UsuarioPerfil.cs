using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace FitLife.Models
{
    public class UsuarioPerfil
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El dni es obligatorio.")]
        [CheckDNI(ErrorMessage = "Formato incorrecto.")]
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

        [Required(ErrorMessage = "La edad es obligatorio.")]
        [Range(18, 90, ErrorMessage = "La edad minima es 18 y máxima 90.")]
        public int Edad { get; set; }

        public string Sexo { get; set; }

        [Required(ErrorMessage = "La altura es obligatoria.")]
        [Range(100, 250, ErrorMessage = "La altura minima es 100cm y maxima de 250cm.")]
        public int Altura { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio.")]
        [Range(30, 200, ErrorMessage = "El peso minimo es de 30 y maximo de 299.")]
        public int Peso { get; set; }
    }

    public class CheckDNI: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string dni = (string)value;
            if (dni.Length != 9)
            {
                return false;
            }

            string dniNumbers = dni.Substring(0, dni.Length - 1);
            string dniLeter = dni.Substring(dni.Length - 1, 1);
            var numbersValid = int.TryParse(dniNumbers, out int dniInteger);

            if (!numbersValid)
            {
                return false;
            }

            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            var mod = dniInteger % 23;
            string letter = control[mod];

            if (letter != dniLeter)
            {
                return false;
            }

            return true;
        }
    }
}
