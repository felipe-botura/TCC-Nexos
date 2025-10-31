using Microsoft.AspNetCore.Identity;
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
        public DbSet<Usuario> Usuarios { get; set; } // se já usa Identity, mantenha apenas IdentityUser-related se quiser
        public DbSet<CampanhaMesa> CampanhasMesas { get; set; }
        public DbSet<Testemunho> Testemunhos { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Reacao> Reacoes { get; set; }
        public DbSet<ChatCampanha> ChatsCampanha { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AppDbSeed.Seed(modelBuilder);
        }

        private static void seedDefaultUser(ModelBuilder builder)
        {
                #region Populate Roles - Perfis de Usuário
                List<IdentityRole> roles = new()
                {
                    new IdentityRole() {
                    Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR"
                    },
                    new IdentityRole() {
                    Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    Name = "Usuário",
                    NormalizedName = "USUÁRIO"
                    },
                };
                builder.Entity<IdentityRole>().HasData(roles);
                #endregion

                #region Populate Usuário
                List<Usuario> usuarios = new() {
                    new Usuario(){
                        Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                        Email = "felipebotura7@gmail.com",
                        NormalizedEmail = "FELIPEBOTURA7@GMAIL.COM",
                        UserName = "felipebotura",
                        NormalizedUserName = "FELIPEBOTURA",
                        LockoutEnabled = true,
                        EmailConfirmed = true,
                        Nome = "Felipe Bissolli Botura"
                    }
                };
                foreach (var user in usuarios)
                {
                        PasswordHasher<IdentityUser> pass = new();
                        user.PasswordHash = pass.HashPassword(user, "123456");
                }
                builder.Entity<Usuario>().HasData(usuarios);
                #endregion

                #region Populate UserRole - Usuário com Perfil
                List<IdentityUserRole<string>> userRoles = new()
                {
                    new IdentityUserRole<string>() {
                        UserId = usuarios[0].Id,
                        RoleId = roles[0].Id
                    }
                };
                builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
                #endregion

        }
    }
}
