using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("Client")]
    public class Client : User
    {
        [Column("edad")]
        public int Age { get; set; }

        [Column("sexo")]
        public string Sexo { get; set; }

        [Column("height")]
        public string Height { get; set; }

        public Client(string name, string lastname, string email, string password, int age, string sexo, string height): 
            base(name, lastname, email, password) 
        {
            this.Age = age;
            this.Sexo = sexo;
            this.Height = height;
        }
    }
}
