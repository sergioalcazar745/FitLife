namespace FitLife.Models
{
    public class AlimentoAñadir
    {
        public int IdAlimentoAñadir { get; set; }
        public string Comida { get; set; }

        public int Alimento { get; set; }

        public int Peso { get; set; }

        public double Kcal { get; set; }

        public double Carbohidratos { get; set; }

        public double Proteinas { get; set; }

        public double Fibra { get; set; }

        public double Grasas { get; set; }
    }
}
