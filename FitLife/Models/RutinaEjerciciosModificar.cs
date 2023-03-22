namespace FitLife.Models
{
    public class RutinaEjerciciosModificar
    {
        public int IdRutina { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public List<int> ListaEliminar { get; set; }
        public List<ModelEjercicio> ListaActualizar { get; set; }
        public List<ModelEjercicio> ListaNuevos { get; set; }
    }
}
