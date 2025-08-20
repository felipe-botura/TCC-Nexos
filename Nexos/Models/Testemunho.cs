using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexos.Models
{
    [Table("TESTEMUNHOS")]
    public class Testemunho
    {
        [Key]
        public int ID_Testemunho { get; set; }

        public string Conteudo_Testemunho { get; set; }

        [Required]
        public int ID_Autor { get; set; }

        [ForeignKey(nameof(ID_Autor))]
        public Usuario Autor { get; set; }

        public DateTime Data_Testemunho { get; set; } = DateTime.UtcNow;
    }
}
