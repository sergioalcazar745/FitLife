using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("EJERCICIO")]
    public class Ejercicio
    {
        [Key]
        [Column("IDEJERCICIO")]
        public int IdEjercicio { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }
    }
}
