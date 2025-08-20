using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexos.Models
{
    [Table("GENEROS")]
    public class Genero
    {
        [Key]
        public int ID_Genero { get; set; }

        [Required, MaxLength(200)]
        public string Nome_Genero { get; set; }

        // Navegação: campanhas associadas ao gênero
        public ICollection<CampanhaMesa> Campanhas { get; set; }
    }
}
