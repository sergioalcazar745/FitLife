using FitLife.Models;

namespace FitLife.Repositories
{
    public interface IRepository
    {
        Usuario Login(string email);

        void Logout(int id);

        Usuario FindUsuario(int idUsuario);

        Usuario FindUsuarioByEmailAndDNI(string email, string dni);

        PerfilUsuario FindPerfilUsuario(int idUsuario);

        Task RegistrarUsuario(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role);

        Task RegistrarCliente(string nombre, string apellidos, string dni, string email, byte[] passwordencrypt, string salt, string password, string role, int altura, int peso, int edad, string sexo);
    }
}
