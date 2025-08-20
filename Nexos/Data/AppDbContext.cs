using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nexos.Models;

namespace Nexos.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Usuario> UsuariosCustom { get; set; } // se já usa Identity, mantenha apenas IdentityUser-related se quiser
        public DbSet<CampanhaMesa> CampanhasMesas { get; set; }
        public DbSet<Testemunho> Testemunhos { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Reacao> Reacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aplica todas as configurações da pasta Data/Configurations (classes que implementam IEntityTypeConfiguration<T>)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
