namespace FitLife.Models
{
    public class ModelCalendario
    {
        public List<RutinaDia> Rutinas { get; set; }

        public Dieta Dieta { get; set; }

        public List<ModelComidaAlimento> ComidaAlimentos { get; set; }
    }
}
