using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Nexos.Models;

[Table("usuario")]
public class Usuario : IdentityUser
{
    [Required(ErrorMessage = "Por favor, informe o nome")]
    [StringLength(60, ErrorMessage = "O nome deve possuir no máximo 60 caracteres")]
    public string Nome { get; set; }
    [Display(Name = "Data de nascimento")]
    [DataType(DataType.Date)]
    public DateTime? DataNascimento { get; set; }
    
    [StringLength(200)]
    public string Foto { get; set; }
}