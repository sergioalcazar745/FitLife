using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("RUTINAS")]
    public class Rutina
    {
        [Key]
        [Column("IDRUTINA")]
        public int IdRutina { get; set; }

        [Column("IDCLIENTE")]
        public int IdCliente{ get; set; }

        [Column("IDENTRENADOR")]
        public int IdEntrenador { get; set; }

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("COMENTARIO")]
        public string Comentario { get; set; }
    }
}
