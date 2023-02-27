namespace FitLife.Models
{
    public class Error
    {
        public Error(string name, string mensaje)
        {
            this.Name = name;
            this.Mensaje = mensaje;
        }
        public string Name { get; set; }
        public string Mensaje { get; set; }
    }
}
