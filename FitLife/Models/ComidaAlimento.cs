using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("COMIDAALIMENTO")]
    public class ComidaAlimento
    {
        [Column("IDCOMIDAALIMENTO")]
        public int IdComidaAlimento { get; set; }

        [Column("IDCOMIDA")]
        public int IdComida { get; set; }

        [Column("IDALIMENTO")]
        public int IdAlimento { get; set; }

        [Column("PESO")]
        public double Peso { get; set; }
    }
}
