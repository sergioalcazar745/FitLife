using FitLife.Models;

namespace FitLife.Repositories
{
    public interface IRepository
    {
        User Login();

        void Logout();

        void RegisterUser(string name, string lastname, string email, string password);
        void RegisterClient(string name, string lastname, string email, string password, int age, int height, string sexo);
    }
}
