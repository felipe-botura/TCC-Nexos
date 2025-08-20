using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexos.Models
{
    [Table("POSTAGENS")]
    public class Postagem
    {
        [Key]
        public int ID_Postagem { get; set; }

        [MaxLength(255)]
        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public DateTime Data_Publicacao { get; set; } = DateTime.UtcNow;

        [Required]
        public int ID_Autor { get; set; }

        [ForeignKey(nameof(ID_Autor))]
        public Usuario Autor { get; set; }

        public int Numero_Comentarios { get; set; }
        public int Numero_Reacoes { get; set; }

        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Reacao> Reacoes { get; set; }
    }
}
