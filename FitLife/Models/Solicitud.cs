using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("SOLICITUD")]
    public class Solicitud
    {
        [Column("SALT")]
        public string Salt { get; set; }

        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
    }
}
