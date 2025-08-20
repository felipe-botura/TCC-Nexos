using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexos.Models
{
    [Table("COMENTARIOS")]
    public class Comentario
    {
        [Key]
        public int ID_Comentario { get; set; }

        public string Conteudo_Comentario { get; set; }

        public DateTime Data_Comentario { get; set; } = DateTime.UtcNow;

        [Required]
        public int ID_Autor { get; set; }

        [ForeignKey(nameof(ID_Autor))]
        public Usuario Autor { get; set; }

        [Required]
        public int ID_Postagem { get; set; }

        [ForeignKey(nameof(ID_Postagem))]
        public Postagem Postagem { get; set; }

        public int Numero_Reacoes { get; set; }

        public ICollection<Reacao> Reacoes { get; set; }
    }
}
