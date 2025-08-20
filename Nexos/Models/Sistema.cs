using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexos.Models
{
    [Table("SISTEMAS")]
    public class Sistema
    {
        [Key]
        public int ID_Sistema { get; set; }

        [Required, MaxLength(200)]
        public string Nome_Sistema { get; set; }

    }
}
