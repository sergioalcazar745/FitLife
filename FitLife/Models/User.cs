using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitLife.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("idUser")]
        public int IdUser { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
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
