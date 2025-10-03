using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nexos.Models;

namespace Nexos.Data
{
    public static class AppDbSeed
    {
        public static void Seed(ModelBuilder builder)
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
                    Nome = "Felipe Bissolli Botura",
                    DataNascimento = new DateTime(1990, 5, 15),
                },
                new Usuario(){
                    Id = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    Email = "usuario2@example.com",
                    NormalizedEmail = "USUARIO2@EXAMPLE.COM",
                    UserName = "usuario2",
                    NormalizedUserName = "USUARIO2",
                    LockoutEnabled = true,
                    EmailConfirmed = true,
                    Nome = "Segundo Usuário",
                    DataNascimento = new DateTime(1992, 8, 20),
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
                },
                new IdentityUserRole<string>() {
                    UserId = usuarios[1].Id,
                    RoleId = roles[1].Id
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion

            #region Populate Generos
            List<Genero> generos = new()
            {
                new Genero { ID_Genero = 1, Nome_Genero = "Fantasia" },
                new Genero { ID_Genero = 2, Nome_Genero = "Ficção Científica" },
                new Genero { ID_Genero = 3, Nome_Genero = "Terror" },
                new Genero { ID_Genero = 4, Nome_Genero = "Aventura" }
            };
            builder.Entity<Genero>().HasData(generos);
            #endregion

            #region Populate Sistemas
            List<Sistema> sistemas = new()
            {
                new Sistema { ID_Sistema = 1, Nome_Sistema = "D&D 5e" },
                new Sistema { ID_Sistema = 2, Nome_Sistema = "Tormenta 20" },
                new Sistema { ID_Sistema = 3, Nome_Sistema = "Call of Cthulhu" }
            };
            builder.Entity<Sistema>().HasData(sistemas);
            #endregion

            #region Populate CampanhasMesas
            List<CampanhaMesa> campanhasMesas = new()
            {
                new CampanhaMesa { ID_Campanha = 1, Titulo_Campanha = "A Ascensão de Tiamat", Premissa_Campanha = "A paz relativa do mundo está por um fio. Cultistas obscuros trabalham nas sombras para libertar Tiamat, a temível deusa dragão de cinco cabeças, aprisionada nas profundezas do plano infernal. À medida que forças malignas começam a se mover, cidades e vilarejos enfrentam ataques de dragões e seguidores fanáticos, espalhando terror por toda a região. Somente um grupo de heróis destemidos pode impedir a ascensão de Tiamat e salvar o mundo da destruição iminente.", O_Que_Esperar = "Muita aventura e desafios.", Imagem_Capa = "/img/campaigns/placeholder.png", Status_Campanha = "Ativa", Vagas_Disponiveis = 4, Faixa_Etaria = "16+", ID_Sistema = 1, ID_Genero = 1, Dias_Horarios = "Terças, 20h", Modalidade = "Online", Plataformas = "Discord, Roll20", Requisitos = "Microfone", Numero_Jogadores = 5, ID_Mestre = usuarios[0].Id, DataCriacao = DateTime.Now },
                new CampanhaMesa { ID_Campanha = 2, Titulo_Campanha = "O Mistério da Mansão Blackwood", Premissa_Campanha = "Uma aventura de terror em Call of Cthulhu.", O_Que_Esperar = "Suspense e investigação.", Imagem_Capa = "/img/campaigns/placeholder2.png", Status_Campanha = "Em breve", Vagas_Disponiveis = 3, Faixa_Etaria = "18+", ID_Sistema = 3, ID_Genero = 3, Dias_Horarios = "Sextas, 21h", Modalidade = "Presencial", Plataformas = "N/A", Requisitos = "Disposição para o terror", Numero_Jogadores = 4, ID_Mestre = usuarios[1].Id, DataCriacao = DateTime.Now.AddDays(-10) }
            };
            builder.Entity<CampanhaMesa>().HasData(campanhasMesas);
            #endregion

            #region Populate Postagens
            List<Postagem> postagens = new()
            {
                new Postagem { ID_Postagem = 1, Titulo = "Dicas para Mestres Iniciantes", Conteudo = "Algumas dicas úteis para quem está começando a mestrar RPG.", Data_Publicacao = DateTime.Now, ID_Autor = usuarios[0].Id, Numero_Comentarios = 0, Numero_Reacoes = 0 },
                new Postagem { ID_Postagem = 2, Titulo = "Resenha: Tormenta 20", Conteudo = "Minhas impressões sobre o sistema Tormenta 20.", Data_Publicacao = DateTime.Now.AddDays(-5), ID_Autor = usuarios[1].Id, Numero_Comentarios = 0, Numero_Reacoes = 0 }
            };
            builder.Entity<Postagem>().HasData(postagens);
            #endregion

            #region Populate Comentarios
            List<Comentario> comentarios = new()
            {
                new Comentario { ID_Comentario = 1, Conteudo_Comentario = "Ótimas dicas!", Data_Comentario = DateTime.Now.AddHours(1), ID_Postagem = 1, ID_Autor = usuarios[1].Id, Numero_Reacoes = 0 },
                new Comentario { ID_Comentario = 2, Conteudo_Comentario = "Concordo plenamente.", Data_Comentario = DateTime.Now.AddHours(2), ID_Postagem = 1, ID_Autor = usuarios[0].Id, Numero_Reacoes = 0 }
            };
            builder.Entity<Comentario>().HasData(comentarios);
            #endregion

            #region Populate Reacoes
            List<Reacao> reacoes = new()
            {
                new Reacao { ID_Reacao = 1, Tipo_Reacao = "Like", ID_Postagem = 1, ID_Usuario = usuarios[1].Id },
                new Reacao { ID_Reacao = 2, Tipo_Reacao = "Love", ID_Postagem = 2, ID_Usuario = usuarios[0].Id }
            };
            builder.Entity<Reacao>().HasData(reacoes);
            #endregion

            #region Populate Testemunhos
            List<Testemunho> testemunhos = new()
            {
                new Testemunho { ID_Testemunho = 1, Conteudo_Testemunho = "O Nexos é uma plataforma incrível para encontrar mesas de RPG!", ID_Autor = "a1b2c3d4-e5f6-7890-1234-567890abcdef", Data_Testemunho = DateTime.Now.AddDays(-20) },
                new Testemunho { ID_Testemunho = 2, Conteudo_Testemunho = "Facilitou muito a organização das minhas campanhas.", ID_Autor = "a1b2c3d4-e5f6-7890-1234-567890abcdef", Data_Testemunho = DateTime.Now.AddDays(-15) }
            };
            builder.Entity<Testemunho>().HasData(testemunhos);
            #endregion
        }
    }
}
