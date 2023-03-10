using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("APELLIDOS")]
        public string Apellidos { get; set; }

        [Column("DNI")]
        public string Dni { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORDENCRYPT")]
        public byte[] PasswordEncrypt { get; set; }

        [Column("SALT")]
        public string Salt { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("ROLE")]
        public string Role { get; set; }

        [Column("ESTADO")]
        public bool Estado { get; set; }
    }
}
