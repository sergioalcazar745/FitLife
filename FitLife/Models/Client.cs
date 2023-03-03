using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("CLIENTE")]
    public class Client : User
    {
        [Column("IDCLIENTE")]
        public int IdCliente { get; set; }

        [Column("EDAD")]
        public int Edad { get; set; }

        [Column("SEXO")]
        public string Sexo { get; set; }

        [Column("ALTURA")]
        public string Altura { get; set; }

        public Client(string name, string lastname, string email, string password, int edad, string sexo, string altura): 
            base(name, lastname, email, password) 
        {
            this.Edad = edad;
            this.Sexo = sexo;
            this.Altura = altura;
        }
    }
}
