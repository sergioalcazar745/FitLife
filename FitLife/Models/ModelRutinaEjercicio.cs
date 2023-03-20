using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    public class ModelRutinaEjercicio
    {
        public string Nombre { get; set; }

        public int Series { get; set; }

        public int Repeticiones { get; set; }

        public int PausaBajada { get; set; }

        public int PausaSubida { get; set; }

        public int PausaAguante { get; set; }

        public int Arroba { get; set; }
    }
}
