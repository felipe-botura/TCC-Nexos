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
                    Foto = "/img/usuarios/felipe.png",
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
                    Premissa_Campanha = "O Culto do Dragão está se movendo nas sombras, reunindo artefatos lendários para invocar Tiamat, a Deusa Dragão de cinco cabeças, de volta ao mundo. Vocês são a única esperança de impedir que o caos e a tirania se espalhem por Faerûn. A jornada será longa e perigosa, levando-os de cidades sitiadas a covis de dragões ancestrais, onde cada decisão pode selar o destino do mundo.",
                    O_Que_Esperar = "Aventura épica e combates intensos.",
                    Imagem_Capa = "/img/campaigns/placeholder1.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 4,
                    Faixa_Etaria = "+16",
                    ID_Sistema = 1,
                    ID_Genero = 1,
                    Dias_Horarios = "Terças, 20h",
                    Modalidade = "Online",
                    Plataformas = "Discord, Roll20",
                    Requisitos = "Microfone",
                    Numero_Jogadores = 5,
                    Categoria = "Campanha longa",
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-1)
                },
                new CampanhaMesa {
                    ID_Campanha = 2,
                    Titulo_Campanha = "O Mistério da Mansão Blackwood",
                    Premissa_Campanha = "A Mansão Blackwood tem sido palco de eventos inexplicáveis e desaparecimentos há décadas. Como investigadores do paranormal, vocês são chamados para desvendar o mistério por trás de sua reputação macabra. Preparem-se para enfrentar não apenas fantasmas e maldições, mas também os segredos sombrios de uma família que se recusou a morrer, mergulhando em um terror psicológico onde a sanidade é o preço da verdade.",
                    O_Que_Esperar = "Terror psicológico e suspense.",
                    Imagem_Capa = "/img/campaigns/placeholder2.png",
                    Status_Campanha = "Em breve",
                    Vagas_Disponiveis = 3,
                    Faixa_Etaria = "+18",
                    ID_Sistema = 3,
                    ID_Genero = 3,
                    Dias_Horarios = "Sextas, 21h",
                    Modalidade = "Presencial",
                    Plataformas = "N/A",
                    Requisitos = "Disposição para o terror",
                    Numero_Jogadores = 4,
                    Categoria = "One-shot",
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-5)
                },
                new CampanhaMesa {
                    ID_Campanha = 3,
                    Titulo_Campanha = "Sombras de Cybercity",
                    Premissa_Campanha = "Em Cybercity, a luz neon esconde a podridão das megacorporações que controlam cada aspecto da vida. Vocês são um grupo de *mercenários* (ou *edgerunners*) contratados para realizar trabalhos sujos e perigosos. Sua missão atual: roubar dados cruciais de uma das maiores corporações, mas o que começa como um simples roubo se transforma em uma conspiração que ameaça expor a verdade por trás do poder da cidade.",
                    O_Que_Esperar = "Ação e intriga em um cenário cyberpunk.",
                    Imagem_Capa = "/img/campaigns/placeholder3.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 5,
                    Faixa_Etaria = "+18",
                    ID_Sistema = 7,
                    ID_Genero = 2,
                    Dias_Horarios = "Sábados, 19h",
                    Modalidade = "Online",
                    Plataformas = "Discord",
                    Requisitos = "Câmera e microfone",
                    Numero_Jogadores = 6,
                    Categoria = "Campanha longa",
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-10)
                },
                new CampanhaMesa {
                    ID_Campanha = 4,
                    Titulo_Campanha = "Reinos Perdidos de Aranthor",
                    Premissa_Campanha = "Aranthor, um reino há muito perdido, ressurge das brumas do tempo. Vocês são exploradores destemidos que se aventuram nas ruínas de uma civilização mágica esquecida, repleta de armadilhas antigas, tesouros incalculáveis e, mais importante, segredos que podem reescrever a história. O que vocês farão com o poder que encontrarem nas profundezas de Aranthor?",
                    O_Que_Esperar = "Exploração e fantasia clássica.",
                    Imagem_Capa = "/img/campaigns/placeholder4.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 4,
                    Faixa_Etaria = "+14",
                    ID_Sistema = 1,
                    ID_Genero = 1,
                    Dias_Horarios = "Domingos, 14h",
                    Modalidade = "Online",
                    Plataformas = "Foundry VTT",
                    Requisitos = "Conhecimento básico do sistema",
                    Numero_Jogadores = 5,
                    Categoria = "Mini-campanha",
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-15)
                },
                new CampanhaMesa {
                    ID_Campanha = 5,
                    Titulo_Campanha = "Sangue e Trevas",
                    Premissa_Campanha = "A noite pertence aos Membros, e a cidade é o tabuleiro de xadrez onde clãs de vampiros disputam poder e influência. Vocês são recém-criados ou veteranos da Camarilla, tentando sobreviver à política traiçoeira, à fome insaciável e à ameaça constante da Inquisição. A premissa é simples: mantenham a Máscara, sobrevivam à noite e tentem não se tornar peões na Guerra da Gehenna.",
                    O_Que_Esperar = "Intriga política e drama sombrio.",
                    Imagem_Capa = "/img/campaigns/placeholder5.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 2,
                    Faixa_Etaria = "+18",
                    ID_Sistema = 6,
                    ID_Genero = 5,
                    Dias_Horarios = "Quartas, 22h",
                    Modalidade = "Online",
                    Plataformas = "Discord",
                    Requisitos = "Interpretação madura",
                    Numero_Jogadores = 4,
                    Categoria = "Campanha longa",
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-20)
                },
                new CampanhaMesa {
                    ID_Campanha = 6,
                    Titulo_Campanha = "Guerra das Relíquias",
                    Premissa_Campanha = "O mundo está à beira de uma nova era de conflito. Relíquias de poder inimaginável foram descobertas, e as grandes nações competem para controlá-las. Vocês são um grupo de aventureiros contratados por uma organização secreta para recuperar esses artefatos antes que caiam em mãos erradas. A cada relíquia encontrada, o destino do mundo se torna mais incerto, exigindo grandes batalhas e decisões políticas difíceis.",
                    O_Que_Esperar = "Grandes batalhas e decisões políticas.",
                    Imagem_Capa = "/img/campaigns/placeholder6.png",
                    Status_Campanha = "Em breve",
                    Vagas_Disponiveis = 5,
                    Faixa_Etaria = "+16",
                    ID_Sistema = 5,
                    ID_Genero = 4,
                    Dias_Horarios = "Segundas, 19h",
                    Modalidade = "Presencial",
                    Plataformas = "N/A",
                    Requisitos = "Disponibilidade semanal",
                    Numero_Jogadores = 6,
                    Categoria = "Campanha longa",
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-25)
                },
                new CampanhaMesa {
                    ID_Campanha = 7,
                    Titulo_Campanha = "A Comédia dos Heróis",
                    Premissa_Campanha = "O reino de Eldoria está em perigo, mas o grupo de heróis convocado para salvá-lo é, digamos, peculiar. Vocês são um bando de desajustados com boas intenções, mas péssima coordenação. A missão é salvar a princesa, mas o caminho é pavimentado com erros hilários, mal-entendidos e a constante ameaça de causar mais dano do que o próprio vilão. Preparem-se para muito humor e situações inusitadas.",
                    O_Que_Esperar = "Muito humor e situações inusitadas.",
                    Imagem_Capa = "/img/campaigns/placeholder7.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 3,
                    Faixa_Etaria = "+12",
                    ID_Sistema = 8,
                    ID_Genero = 6,
                    Dias_Horarios = "Domingos, 18h",
                    Modalidade = "Online",
                    Plataformas = "Discord",
                    Requisitos = "Boa vontade para rir",
                    Numero_Jogadores = 5,
                    Categoria = "Mini-campanha",
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-30)
                },
                new CampanhaMesa {
                    ID_Campanha = 8,
                    Titulo_Campanha = "O Segredo do Tempo",
                    Premissa_Campanha = "O tecido do tempo está se desfazendo, e eventos do passado e do futuro se misturam no presente. Vocês são investigadores especializados em anomalias temporais, chamados para descobrir a causa desse colapso. A aventura os levará a desvendar mistérios e quebra-cabeças temporais, onde cada pista pode estar em uma época diferente, e a falha significa a aniquilação da própria realidade.",
                    O_Que_Esperar = "Mistérios e quebra-cabeças temporais.",
                    Imagem_Capa = "/img/campaigns/placeholder8.png",
                    Status_Campanha = "Ativa",
                    Vagas_Disponiveis = 4,
                    Faixa_Etaria = "+16",
                    ID_Sistema = 2,
                    ID_Genero = 7,
                    Dias_Horarios = "Sábados, 20h",
                    Modalidade = "Online",
                    Plataformas = "Discord, Roll20",
                    Requisitos = "Gosto por enigmas",
                    Numero_Jogadores = 5,
                    Categoria = "Mini-campanha",
                    ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                    DataCriacao = DateTime.Now.AddDays(-35)
                },
                new CampanhaMesa {
                    ID_Campanha = 9,
                    Titulo_Campanha = "Crônicas de Steamport",
                    Premissa_Campanha = "Bem-vindos a Steamport, a joia da coroa da era do vapor. Sob a fumaça das chaminés e o brilho do latão, heróis e inventores lutam contra conspirações industriais e a corrupção da elite. Vocês são a linha de frente contra aqueles que usam a tecnologia para oprimir. Preparem-se para uma aventura de ação, tecnologia e política, onde a engrenagem mais importante é a sua coragem.",
                    O_Que_Esperar = "Aventura, tecnologia e política.",
                    Imagem_Capa = "/img/campaigns/placeholder9.png",
                    Status_Campanha = "Em breve",
                    Vagas_Disponiveis = 5,
                    Faixa_Etaria = "+14",
                    ID_Sistema = 9,
                    ID_Genero = 9,
                    Dias_Horarios = "Quintas, 21h",
                    Modalidade = "Presencial",
                    Plataformas = "N/A",
                    Requisitos = "Curiosidade e criatividade",
                    Numero_Jogadores = 6,
                    Categoria = "Campanha longa",
                    ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                    DataCriacao = DateTime.Now.AddDays(-40)
                },
                new CampanhaMesa {
            ID_Campanha = 10,
            Titulo_Campanha = "Ecos do Abismo",
            Premissa_Campanha = "Nas profundezas do oceano, um culto esquecido está prestes a romper o selo que aprisiona uma entidade cósmica ancestral. Vocês são investigadores de eventos sobrenaturais, e a missão é clara: impedir o ritual antes que o horror indescritível seja liberado. Esta é uma aventura de terror cósmico e investigação intensa, onde a sanidade é um luxo que vocês não podem pagar.",
            O_Que_Esperar = "Terror cósmico e investigação intensa.",
            Imagem_Capa = "/img/campaigns/placeholder10.png",
            Status_Campanha = "Ativa",
            Vagas_Disponiveis = 3,
            Faixa_Etaria = "+18",
            ID_Sistema = 3,
            ID_Genero = 3,
            Dias_Horarios = "Sábados, 22h",
            Modalidade = "Online",
            Plataformas = "Discord",
            Requisitos = "Interpretação madura",
            Numero_Jogadores = 4,
            Categoria = "One-shot",
            ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
            DataCriacao = DateTime.Now.AddDays(-45)
        },
        new CampanhaMesa {
            ID_Campanha = 11,
            Titulo_Campanha = "Tempestade Carmesim",
            Premissa_Campanha = "Os mares do reino estão sob ataque de uma frota pirata misteriosa, conhecida apenas como Tempestade Carmesim. Vocês são marinheiros experientes e aventureiros contratados para descobrir a origem desses ataques e detê-los. Preparem-se para batalhas navais épicas, exploração de ilhas desconhecidas e a busca por um tesouro que pode ser a chave para a paz ou a ruína do reino.",
            O_Que_Esperar = "Batalhas navais e aventuras marítimas.",
            Imagem_Capa = "/img/campaigns/placeholder11.png",
            Status_Campanha = "Em breve",
            Vagas_Disponiveis = 5,
            Faixa_Etaria = "+14",
            ID_Sistema = 1,
            ID_Genero = 1,
            Dias_Horarios = "Quartas, 19h",
            Modalidade = "Presencial",
            Plataformas = "N/A",
            Requisitos = "Espírito aventureiro",
            Numero_Jogadores = 6,
            Categoria = "Mini-campanha",
            ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
            DataCriacao = DateTime.Now.AddDays(-50)
        },
        new CampanhaMesa {
            ID_Campanha = 12,
            Titulo_Campanha = "Neon & Caos",
            Premissa_Campanha = "Em Neo-Kyoto, a metrópole iluminada por neon, a linha entre o legal e o ilegal é tênue. Vocês são hackers e rebeldes que vivem à margem, lutando contra o domínio das megacorporações. Sua vida é uma série de missões arriscadas e conspirações tecnológicas, onde a única regra é não ser pego. A sobrevivência depende da sua habilidade de navegar no caos digital e nas ruas escuras da cidade.",
            O_Que_Esperar = "Missões arriscadas e conspirações tecnológicas.",
            Imagem_Capa = "/img/campaigns/placeholder12.png",
            Status_Campanha = "Ativa",
            Vagas_Disponiveis = 4,
            Faixa_Etaria = "+18",
            ID_Sistema = 7,
            ID_Genero = 2,
            Dias_Horarios = "Sextas, 20h",
            Modalidade = "Online",
            Plataformas = "Discord",
            Requisitos = "Microfone e boa conexão",
            Numero_Jogadores = 5,
            Categoria = "Campanha longa",
            ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
            DataCriacao = DateTime.Now.AddDays(-55)
        },
        new CampanhaMesa {
            ID_Campanha = 13,
            Titulo_Campanha = "O Reino dos Sonhos",
            Premissa_Campanha = "Acordar em um mundo onde seus sonhos (e pesadelos) se manifestam é a nova realidade de vocês. Transportados para o Reino dos Sonhos, vocês devem aprender a controlar a própria imaginação para sobreviver. Esta é uma aventura de exploração surreal e desafios criativos, onde a realidade é fluida e a única limitação é a sua mente.",
            O_Que_Esperar = "Exploração surreal e desafios criativos.",
            Imagem_Capa = "/img/campaigns/placeholder13.png",
            Status_Campanha = "Ativa",
            Vagas_Disponiveis = 5,
            Faixa_Etaria = "+12",
            ID_Sistema = 1,
            ID_Genero = 7,
            Dias_Horarios = "Domingos, 10h",
            Modalidade = "Online",
            Plataformas = "Foundry VTT",
            Requisitos = "Imaginação livre",
            Numero_Jogadores = 6,
            Categoria = "Mini-campanha",
            ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
            DataCriacao = DateTime.Now.AddDays(-60)
        },
        new CampanhaMesa {
            ID_Campanha = 14,
            Titulo_Campanha = "Sombras de Avalon",
            Premissa_Campanha = "O reino de Avalon está dividido por intrigas mágicas e traições na corte. Inspirados nas lendas arturianas, vocês são cavaleiros e magos que devem navegar pela política traiçoeira para salvar o reino de uma guerra civil iminente. Preparem-se para um drama político e aventuras épicas, onde a lealdade é testada a cada passo.",
            O_Que_Esperar = "Drama político e aventuras épicas.",
            Imagem_Capa = "/img/campaigns/placeholder14.png",
            Status_Campanha = "Em breve",
            Vagas_Disponiveis = 3,
            Faixa_Etaria = "+16",
            ID_Sistema = 5,
            ID_Genero = 4,
            Dias_Horarios = "Segundas, 20h",
            Modalidade = "Presencial",
            Plataformas = "N/A",
            Requisitos = "Disponibilidade constante",
            Numero_Jogadores = 4,
            Categoria = "Campanha longa",
            ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
            DataCriacao = DateTime.Now.AddDays(-65)
        },
        new CampanhaMesa {
            ID_Campanha = 15,
            Titulo_Campanha = "Caçadores do Apocalipse",
            Premissa_Campanha = "O mundo como o conhecíamos acabou. Agora, vocês são sobreviventes em um cenário pós-apocalíptico infestado de monstros e facções rivais. A cada dia é uma luta pela sobrevivência, exigindo ação intensa e horror de sobrevivência. A busca por suprimentos e um lugar seguro é constante, e a humanidade está à beira da extinção.",
            O_Que_Esperar = "Ação intensa e horror de sobrevivência.",
            Imagem_Capa = "/img/campaigns/placeholder15.png",
            Status_Campanha = "Ativa",
            Vagas_Disponiveis = 2,
            Faixa_Etaria = "+18",
            ID_Sistema = 6,
            ID_Genero = 5,
            Dias_Horarios = "Terças, 22h",
            Modalidade = "Online",
            Plataformas = "Discord",
            Requisitos = "Interpretação intensa",
            Numero_Jogadores = 4,
            Categoria = "Campanha longa",
            ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
            DataCriacao = DateTime.Now.AddDays(-70)
        },
        new CampanhaMesa {
            ID_Campanha = 16,
            Titulo_Campanha = "O Torneio das Mil Lâminas",
            Premissa_Campanha = "O Torneio das Mil Lâminas é o evento mais prestigiado do continente, reunindo os guerreiros mais habilidosos em uma competição de vida ou morte. Vocês são competidores com diferentes motivações: glória, riqueza ou a chance de mudar o destino. Preparem-se para combates táticos e o desenvolvimento de personagens, onde apenas o mais forte (ou mais sortudo) sobreviverá.",
            O_Que_Esperar = "Combates táticos e desenvolvimento de personagens.",
            Imagem_Capa = "/img/campaigns/placeholder16.png",
            Status_Campanha = "Ativa",
            Vagas_Disponiveis = 4,
            Faixa_Etaria = "+14",
            ID_Sistema = 1,
            ID_Genero = 4,
            Dias_Horarios = "Sábados, 15h",
            Modalidade = "Online",
            Plataformas = "Roll20",
            Requisitos = "Conhecimento básico do sistema",
            Numero_Jogadores = 5,
            Categoria = "One-shot",
            ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
            DataCriacao = DateTime.Now.AddDays(-75)
        },
        new CampanhaMesa {
            ID_Campanha = 17,
            Titulo_Campanha = "Entre Estrelas",
            Premissa_Campanha = "A bordo de uma nave estelar, vocês são exploradores viajando pelo espaço em busca de novos mundos habitáveis e segredos de civilizações antigas. A vastidão do cosmos apresenta perigos e dilemas morais, onde cada descoberta pode mudar a compreensão da humanidade sobre o universo. Esta é uma campanha de exploração espacial e descobertas científicas.",
            O_Que_Esperar = "Exploração espacial e dilemas morais.",
            Imagem_Capa = "/img/campaigns/placeholder17.png",
            Status_Campanha = "Em breve",
            Vagas_Disponiveis = 5,
            Faixa_Etaria = "+14",
            ID_Sistema = 4,
            ID_Genero = 8,
            Dias_Horarios = "Quintas, 19h",
            Modalidade = "Online",
            Plataformas = "Discord",
            Requisitos = "Curiosidade científica",
            Numero_Jogadores = 6,
            Categoria = "Campanha longa",
            ID_Mestre = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
            DataCriacao = DateTime.Now.AddDays(-80)
        },
        new CampanhaMesa {
            ID_Campanha = 18,
            Titulo_Campanha = "Lendas de Yggdrasil",
            Premissa_Campanha = "O Fimbulvetr chegou, e os sinais do Ragnarök são inegáveis. Vocês são heróis nórdicos, escolhidos pelos próprios deuses para lutar contra gigantes, monstros e as forças do caos. A missão é evitar o crepúsculo dos deuses, exigindo heroísmo, mitologia e combates épicos contra as forças que ameaçam destruir os Nove Mundos.",
            O_Que_Esperar = "Mitologia, heroísmo e combates épicos.",
            Imagem_Capa = "/img/campaigns/placeholder18.png",
            Status_Campanha = "Ativa",
            Vagas_Disponiveis = 3,
            Faixa_Etaria = "+16",
            ID_Sistema = 1,
            ID_Genero = 1,
            Dias_Horarios = "Domingos, 16h",
            Modalidade = "Online",
            Plataformas = "Foundry VTT",
            Requisitos = "Paixão por mitologia",
            Numero_Jogadores = 5,
            Categoria = "Campanha longa",
            ID_Mestre = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
            DataCriacao = DateTime.Now.AddDays(-85)
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
