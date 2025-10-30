using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexos.Models
{
    [Table("CAMPANHAS_MESAS")]
    public class CampanhaMesa
    {
        [Key]
        public int ID_Campanha { get; set; }

        [Required, MaxLength(255)]
        public string Titulo_Campanha { get; set; }

        // FK para Usuario (mestre)
        [Required]
        public string ID_Mestre { get; set; }

        [ForeignKey(nameof(ID_Mestre))]
        public Usuario Mestre { get; set; }
        
        [Required]
        public string Premissa_Campanha { get; set; }

        [Required]
        public string O_Que_Esperar { get; set; }

        public string Imagem_Capa { get; set; }

        [Required]
        public string Status_Campanha { get; set; }


        public string Categoria { get; set; }

        [Required]
        public int Vagas_Disponiveis { get; set; }

        [Required]
        public string Faixa_Etaria { get; set; }

        // FK para Sistema e Genero (opcionais)
        public int? ID_Sistema { get; set; } // Corrigido para int?

        [ForeignKey(nameof(ID_Sistema))]
        public Sistema Sistema { get; set; }

        public int? ID_Genero { get; set; }

        [ForeignKey(nameof(ID_Genero))]
        public Genero Genero { get; set; }

        [Required]
        public string Dias_Horarios { get; set; }

        [Required]
        public string Modalidade { get; set; }

        public string Plataformas { get; set; }

        public string Requisitos { get; set; }
        
        [Required]
        public int Numero_Jogadores { get; set; }

        public DateTime DataCriacao { get; set; }

    }
}
