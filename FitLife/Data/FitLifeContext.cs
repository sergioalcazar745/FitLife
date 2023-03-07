using FitLife.Models;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Data
{
    public class FitLifeContext: DbContext
    {
        public FitLifeContext(DbContextOptions<FitLifeContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<PerfilUsuario> PerfilUsuarios { get; set; }
    }
}
