using Microsoft.Extensions.Primitives;

namespace FitLife.Models
{
    public class Opcion
    {
        public string Value { get; set; }
        public string Texto { get; set; }

        public Opcion() { }
        public Opcion(string value, string texto)
        {
            this.Value = value;
            this.Texto = texto;
        }
    }
}
