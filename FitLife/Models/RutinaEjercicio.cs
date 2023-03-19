using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("RUTINAEJERCICIO")]
    public class RutinaEjercicio
    {
        [Key]
        [Column("IDRUTINAEJERCICIO")]
        public int IdRutinaEjercicio { get; set; }

        [Column("IDRUTINA")]
        public int IdRutina { get; set; }

        [Column("IDEJERCICIO")]
        public int IdEjercicio { get; set; }

        [Column("SERIES")]
        public int Series { get; set; }

        [Column("REPETICIONES")]
        public int Repeticiones { get; set; }

        [Column("PAUSABAJADA")]
        public int PausaBajada { get; set; }

        [Column("PAUSASUBIDA")]
        public int PausaSubida { get; set; }

        [Column("PAUSAAGUANTE")]
        public int PausaAguante { get; set; }

        [Column("ARROBA")]
        public int Arroba { get; set; }
    }
}
