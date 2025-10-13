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
                    Email = "hemi@gmail.com",
                    NormalizedEmail = "HEMI@GMAIL.COM",
                    UserName = "Hemi",
                    NormalizedUserName = "HEMI",
                    LockoutEnabled = true,
                    EmailConfirmed = true,
                    Nome = "Hemi Domiciano",
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
                new Genero { ID_Genero = 4, Nome_Genero = "Aventura" },
                new Genero { ID_Genero = 5, Nome_Genero = "Drama" },
                new Genero { ID_Genero = 6, Nome_Genero = "Comédia" },
                new Genero { ID_Genero = 7, Nome_Genero = "Mistério" },
                new Genero { ID_Genero = 8, Nome_Genero = "Histórico" },
                new Genero { ID_Genero = 9, Nome_Genero = "Steampunk" }
            };
            builder.Entity<Genero>().HasData(generos);
            #endregion

            #region Populate Sistemas
            List<Sistema> sistemas = new()
            {
                new Sistema { ID_Sistema = 1, Nome_Sistema = "D&D 5e" },
                new Sistema { ID_Sistema = 2, Nome_Sistema = "Tormenta 20" },
                new Sistema { ID_Sistema = 3, Nome_Sistema = "Call of Cthulhu" },
                new Sistema { ID_Sistema = 4, Nome_Sistema = "GURPS" },
                new Sistema { ID_Sistema = 5, Nome_Sistema = "Pathfinder 2e" },
                new Sistema { ID_Sistema = 6, Nome_Sistema = "Vampiro: A Máscara" },
                new Sistema { ID_Sistema = 7, Nome_Sistema = "Cyberpunk RED" },
                new Sistema { ID_Sistema = 8, Nome_Sistema = "Savage Worlds" },
                new Sistema { ID_Sistema = 9, Nome_Sistema = "13th Age" }
            };
            builder.Entity<Sistema>().HasData(sistemas);
            #endregion

            #region Populate CampanhasMesas
            List<CampanhaMesa> campanhasMesas = new()
            {
                new CampanhaMesa {
                    ID_Campanha = 1,
                    Titulo_Campanha = "A Ascensão de Tiamat",
                    Premissa_Campanha = "Cultistas trabalham para libertar Tiamat, a deusa dragão, e os heróis precisam impedir.",
                    O_Que_Esperar = "Aventura épica e combates intensos.",
                    Imagem_Capa = "/img/campaigns/placeholder1.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 4,
                    Faixa_Etaria = "16+",
                    ID_Sistema = 1,
                    ID_Genero = 1,
                    Dias_Horarios = "Terças, 20h",
                    Modalidade = "Online",
                    Plataformas = "Discord, Roll20",
                    Requisitos = "Microfone",
                    Numero_Jogadores = 5,
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-1)
                },
                new CampanhaMesa {
                    ID_Campanha = 2,
                    Titulo_Campanha = "O Mistério da Mansão Blackwood",
                    Premissa_Campanha = "Uma investigação macabra em uma mansão amaldiçoada.",
                    O_Que_Esperar = "Terror psicológico e suspense.",
                    Imagem_Capa = "/img/campaigns/placeholder2.png",
                    Status_Campanha = "Em breve",
                    Vagas_Disponiveis = 3,
                    Faixa_Etaria = "18+",
                    ID_Sistema = 3,
                    ID_Genero = 3,
                    Dias_Horarios = "Sextas, 21h",
                    Modalidade = "Presencial",
                    Plataformas = "N/A",
                    Requisitos = "Disposição para o terror",
                    Numero_Jogadores = 4,
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-5)
                },
                new CampanhaMesa {
                    ID_Campanha = 3,
                    Titulo_Campanha = "Sombras de Cybercity",
                    Premissa_Campanha = "Mercenários enfrentam megacorporações e conspirações em um futuro sombrio.",
                    O_Que_Esperar = "Ação e intriga em um cenário cyberpunk.",
                    Imagem_Capa = "/img/campaigns/placeholder3.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 5,
                    Faixa_Etaria = "18+",
                    ID_Sistema = 7,
                    ID_Genero = 2,
                    Dias_Horarios = "Sábados, 19h",
                    Modalidade = "Online",
                    Plataformas = "Discord",
                    Requisitos = "Câmera e microfone",
                    Numero_Jogadores = 6,
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-10)
                },
                new CampanhaMesa {
                    ID_Campanha = 4,
                    Titulo_Campanha = "Reinos Perdidos de Aranthor",
                    Premissa_Campanha = "Exploradores descobrem ruínas de uma civilização mágica esquecida.",
                    O_Que_Esperar = "Exploração e fantasia clássica.",
                    Imagem_Capa = "/img/campaigns/placeholder4.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 4,
                    Faixa_Etaria = "14+",
                    ID_Sistema = 1,
                    ID_Genero = 1,
                    Dias_Horarios = "Domingos, 14h",
                    Modalidade = "Online",
                    Plataformas = "Foundry VTT",
                    Requisitos = "Conhecimento básico do sistema",
                    Numero_Jogadores = 5,
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-15)
                },
                new CampanhaMesa {
                    ID_Campanha = 5,
                    Titulo_Campanha = "Sangue e Trevas",
                    Premissa_Campanha = "Clãs de vampiros disputam poder nas sombras da cidade.",
                    O_Que_Esperar = "Intriga política e drama sombrio.",
                    Imagem_Capa = "/img/campaigns/placeholder5.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 2,
                    Faixa_Etaria = "18+",
                    ID_Sistema = 6,
                    ID_Genero = 5,
                    Dias_Horarios = "Quartas, 22h",
                    Modalidade = "Online",
                    Plataformas = "Discord",
                    Requisitos = "Interpretação madura",
                    Numero_Jogadores = 4,
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-20)
                },
                new CampanhaMesa {
                    ID_Campanha = 6,
                    Titulo_Campanha = "Guerra das Relíquias",
                    Premissa_Campanha = "Nações competem por artefatos lendários que podem mudar o destino do mundo.",
                    O_Que_Esperar = "Grandes batalhas e decisões políticas.",
                    Imagem_Capa = "/img/campaigns/placeholder6.png",
                    Status_Campanha = "Em breve",
                    Vagas_Disponiveis = 5,
                    Faixa_Etaria = "16+",
                    ID_Sistema = 5,
                    ID_Genero = 4,
                    Dias_Horarios = "Segundas, 19h",
                    Modalidade = "Presencial",
                    Plataformas = "N/A",
                    Requisitos = "Disponibilidade semanal",
                    Numero_Jogadores = 6,
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-25)
                },
                new CampanhaMesa {
                    ID_Campanha = 7,
                    Titulo_Campanha = "A Comédia dos Heróis",
                    Premissa_Campanha = "Um grupo desastrado tenta salvar o reino, causando mais problemas que soluções.",
                    O_Que_Esperar = "Muito humor e situações inusitadas.",
                    Imagem_Capa = "/img/campaigns/placeholder7.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 3,
                    Faixa_Etaria = "12+",
                    ID_Sistema = 8,
                    ID_Genero = 6,
                    Dias_Horarios = "Domingos, 18h",
                    Modalidade = "Online",
                    Plataformas = "Discord",
                    Requisitos = "Boa vontade para rir",
                    Numero_Jogadores = 5,
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-30)
                },
                new CampanhaMesa {
                    ID_Campanha = 8,
                    Titulo_Campanha = "O Segredo do Tempo",
                    Premissa_Campanha = "Aventura investigativa em um mundo onde o tempo está se rompendo.",
                    O_Que_Esperar = "Mistérios e quebra-cabeças temporais.",
                    Imagem_Capa = "/img/campaigns/placeholder8.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 4,
                    Faixa_Etaria = "16+",
                    ID_Sistema = 2,
                    ID_Genero = 7,
                    Dias_Horarios = "Sábados, 20h",
                    Modalidade = "Online",
                    Plataformas = "Discord, Roll20",
                    Requisitos = "Gosto por enigmas",
                    Numero_Jogadores = 5,
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-35)
                },
                new CampanhaMesa {
                    ID_Campanha = 9,
                    Titulo_Campanha = "Crônicas de Steamport",
                    Premissa_Campanha = "Heróis em uma cidade steampunk enfrentam conspirações industriais.",
                    O_Que_Esperar = "Aventura, tecnologia e política.",
                    Imagem_Capa = "/img/campaigns/placeholder9.png",
                    Status_Campanha = "Em breve",
                    Vagas_Disponiveis = 5,
                    Faixa_Etaria = "14+",
                    ID_Sistema = 9,
                    ID_Genero = 9,
                    Dias_Horarios = "Quintas, 21h",
                    Modalidade = "Presencial",
                    Plataformas = "N/A",
                    Requisitos = "Curiosidade e criatividade",
                    Numero_Jogadores = 6,
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-40)
                }
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
