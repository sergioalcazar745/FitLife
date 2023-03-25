using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    public class ModelComidaAlimentoNombre
    {

        public string Alimento { get; set; }

        public double Peso { get; set; }

        public double Kcal { get; set; }

        public double Carbohidratos { get; set; }

        public double Proteinas { get; set; }

        public double Grasas { get; set; }

        public double Fibra { get; set; }
    }
}
