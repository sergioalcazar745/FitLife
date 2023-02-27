using FitLife.Models;

namespace FitLife.Repositories
{
    public interface IRepository
    {
        User Login();

        void Logout();
    }
}
