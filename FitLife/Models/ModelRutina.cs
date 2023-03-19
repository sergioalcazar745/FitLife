namespace FitLife.Models
{
    public class ModelRutina
    {
        public string Fecha { get; set; }
        public List<ModelEjercicio> Ejercicios { get; set; }
        public int IdCliente { get; set; }
    }
}
