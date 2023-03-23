using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("DIETA")]
    public class Dieta
    {
        [Column("IDDIETA")]
        public int IdDieta { get; set; }

        [Column("IDNUTRICIONISTA")]
        public int IdNutricionista { get; set; }

        [Column("IDCLIENTE")]
        public int IdCliente { get; set; }

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("COMENTARIO")]
        public string Comentario { get; set; }
    }
}
