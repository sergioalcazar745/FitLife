namespace FitLife.Models
{
    public class InputRegistro
    {
        public string NombreInput { get; set; }

        public string Error { get; set; }

        public string Tipo { get; set; }

        public List<Opcion> Opciones { get; set; }

        public InputRegistro()
        {}

        public InputRegistro(string nombreInput, string tipo)
        {
            this.NombreInput = nombreInput;
            this.Tipo = tipo;
        }

        public InputRegistro(string nombreInput, string error, string tipo)
        {
            this.NombreInput = nombreInput;
            this.Error = error;
            this.Tipo = tipo;
        }

        public InputRegistro(string nombreInput, string tipo, List<Opcion> opciones)
        {
            this.NombreInput = nombreInput;
            this.Tipo = tipo;
            this.Opciones = opciones;
        }

        public InputRegistro(string nombreInput, string error, string tipo, List<Opcion> opciones)
        {
            this.NombreInput = nombreInput;
            this.Error = error;
            this.Tipo = tipo;
            this.Opciones = opciones;
        }
    }
}
