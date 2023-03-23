using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("COMIDA")]
    public class Comida
    {
        [Column("IDCOMIDA")]
        public int IdComida { get; set; }

        [Column("IDDIETA")]
        public int IdDieta { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("TOTALKCAL")]
        public double TotalKcal { get; set; }

        [Column("COMENTARIO")]
        public string Comentario { get; set; }
    }
}
