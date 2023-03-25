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
        public double Peso { get; set; }

        [Column("KCAL")]
        public double Kcal { get; set; }

        [Column("CARBOHIDRATOS")]
        public double Carbohidratos { get; set; }

        [Column("PROTEINAS")]
        public double Proteinas { get; set; }

        [Column("GRASAS")]
        public double Grasas { get; set; }

        [Column("FIBRA")]
        public double Fibra { get; set; }
    }
}
