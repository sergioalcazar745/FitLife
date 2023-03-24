namespace FitLife.Models
{
    public class AlimentoAñadir
    {
        public int IdAlimentoAñadir { get; set; }
        public string Comida { get; set; }

        public int Alimento { get; set; }

        public int Peso { get; set; }

        public float Kcal { get; set; }

        public float Carbohidratos { get; set; }

        public float Proteinas { get; set; }

        public float Fibra { get; set; }

        public float Grasas { get; set; }
    }
}
