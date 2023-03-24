using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("ALIMENTO")]
    public class Alimento
    {
        [Key]
        [Column("IDALIMENTO")]
        public int IdAlimento { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("PESO")]
        public float Peso { get; set; }

        [Column("KCAL")]
        public float Kcal { get; set; }

        [Column("CARBOHIDRATOS")]
        public float Carbohidratos { get; set; }

        [Column("PROTEINAS")]
        public float Proteinas { get;set; }

        [Column("GRASAS")]
        public float Grasas { get; set; }

        [Column("FIBRA")]
        public float Fibra { get; set; }
    }
}
