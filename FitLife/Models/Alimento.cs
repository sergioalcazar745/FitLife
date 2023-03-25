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
        public double Peso { get; set; }

        [Column("KCAL")]
        public double Kcal { get; set; }

        [Column("CARBOHIDRATOS")]
        public double Carbohidratos { get; set; }

        [Column("PROTEINAS")]
        public double Proteinas { get;set; }

        [Column("GRASAS")]
        public double Grasas { get; set; }

        [Column("FIBRA")]
        public double Fibra { get; set; }
    }
}
