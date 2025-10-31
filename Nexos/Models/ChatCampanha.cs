using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Nexos.Models
{
    [Table("CHAT_CAMPANHA")]
    public class ChatCampanha
    {
        [Key]
        public int Id { get; set; }

        // FK para Usuario (quem enviou a mensagem)
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public Usuario User { get; set; }

        // FK para CampanhaMesa (o grupo de chat)
        [Required]
        public int IdCampanha { get; set; }

        [ForeignKey(nameof(IdCampanha))]
        public CampanhaMesa Campanha { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required, MaxLength(1000)]
        public string Texto { get; set; }

        // Campo para diferenciar Mestre/Jogador (opcional, mas útil para estilização/lógica)
        // 0 = Jogador, 1 = Mestre
        [Required]
        public int TipoUsuario { get; set; }
    }
}
