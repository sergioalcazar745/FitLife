using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("SOLICITUD")]
    public class Solicitud
    {
        [Key]
        [Column("IDSOLICITUD")]
        public int IdSolicitud { get; set; }

        [Column("SALT")]
        public string Salt { get; set; }


        [Column("CODIGO")]
        public int Codigo { get; set; }

        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
    }
}
