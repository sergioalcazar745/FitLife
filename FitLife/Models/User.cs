namespace FitLife.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string name, string lastname, string email, string password)
        {
            this.Name = name;
            this.LastName = lastname;
            this.Email = email;
            this.Password = password;
        }
    }
}
