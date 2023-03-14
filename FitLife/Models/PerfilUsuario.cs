using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("PERFILUSUARIO")]
    public class PerfilUsuario
    {
        [Key]
        [Column("IDPERFILUSUARIO")]
        public int IdPerfilUsuario { get; set; }

        [Column("EDAD")]
        public int Edad { get; set; }

        [Column("SEXO")]
        public string Sexo { get; set; }

        [Column("ALTURA")]
        public int Altura { get; set; }

        [Column("PESO")]
        public int Peso { get; set; }

        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }

        [Column("IDENTRENADOR")]
        public int IdEntrenador { get; set; }

        [Column("IDNUTRICIONISTA")]
        public int IdNutricionista { get; set; }
    }
}
