using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    public class RutinaDia
    {
        public int IdRutina { get; set; }

        public string Nombre { get; set; }

        public DateTime Fecha { get; set; }

        public string Comentario { get; set; }

        public string Ejercicio { get; set; }

        public int Series { get; set; }

        public int Repeticiones { get; set; }

        public int PausaBajada { get; set; }

        public int PausaSubida { get; set; }

        public int PausaAguante { get; set; }

        public int Arroba { get; set; }
    }
}
