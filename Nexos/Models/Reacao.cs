using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexos.Models
{
    [Table("REACOES")]
    public class Reacao
    {
        [Key]
        public int ID_Reacao { get; set; }

        [MaxLength(50)]
        public string Tipo_Reacao { get; set; }

        [Required]
        public int ID_Usuario { get; set; }

        [ForeignKey(nameof(ID_Usuario))]
        public Usuario Usuario { get; set; }

        // Uma reação refere-se ou a uma postagem ou a um comentário (uma das duas FK deve ser não-nula)
        public int? ID_Postagem { get; set; }

        [ForeignKey(nameof(ID_Postagem))]
        public Postagem Postagem { get; set; }

        public int? ID_Comentario { get; set; }

        [ForeignKey(nameof(ID_Comentario))]
        public Comentario Comentario { get; set; }
    }
}
