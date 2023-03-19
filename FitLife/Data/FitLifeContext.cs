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

        public DbSet<Solicitud> Solicitud { get; set; }

        public DbSet<Ejercicio> Ejercicios { get; set; }

        public DbSet<RutinaEjercicio> RutinaEjercicios { get; set; }

        public DbSet<Rutina> Rutinas { get; set; }
    }
}
