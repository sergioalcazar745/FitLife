using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("COMIDAALIMENTO")]
    public class ComidaAlimento
    {
        [Key]
        [Column("IDCOMIDAALIMENTO")]
        public int IdComidaAlimento { get; set; }

        [Column("IDDIETA")]
        public int IdDieta { get; set; }

        [Column("IDCOMIDA")]
        public int IdComida { get; set; }

        [Column("IDALIMENTO")]
        public int IdAlimento { get; set; }

        [Column("PESO")]
        public int Peso { get; set; }

        [Column("KCAL")]
        public float Kcal { get; set; }

        [Column("CARBOHIDRATOS")]
        public float Carbohidratos { get; set; }

        [Column("PROTEINAS")]
        public float Proteinas { get; set; }

        [Column("GRASAS")]
        public float Grasas { get; set; }

        [Column("FIBRA")]
        public float Fibra { get; set; }
    }
}
