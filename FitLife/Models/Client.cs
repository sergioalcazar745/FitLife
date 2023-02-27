namespace FitLife.Models
{
    public class Client : User
    {
        public int Edad { get; set; }
        public string Sexo { get; set; }

        public Client(string name, string lastname, string email, string password, int edad, string sexo): 
            base(name, lastname, email, password) 
        {
            this.Edad = edad;
            this.Sexo = sexo;
        }
    }
}
