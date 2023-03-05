namespace FitLife.Models
{
    public class Error
    {
        public string NombreInput { get; set; }
        public string Mensaje { get; set; }

        public Error() { }

        public Error(string nombreInput, string mensaje)
        {
            this.NombreInput = nombreInput;
            this.Mensaje = mensaje;
        }
    }
}
