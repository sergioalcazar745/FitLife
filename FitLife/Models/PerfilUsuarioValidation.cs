using System.ComponentModel.DataAnnotations;

namespace FitLife.Models
{
    public class PerfilUsuarioValidation
    {
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
}
