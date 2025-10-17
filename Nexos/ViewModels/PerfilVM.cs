using System.ComponentModel.DataAnnotations;

namespace Nexos.ViewModels
{
    public class PerfilVM
    {
        [Display(Name = "Nome Completo", Prompt = "Informe seu Nome Completo")]
        [Required(ErrorMessage = "Por favor, informe seu Nome")]
        [StringLength(60, ErrorMessage = "O Nome deve possuir no máximo 60 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Por favor, informe seu Email")]
        [EmailAddress(ErrorMessage = "Por favor, informe um Email Válido!")]
        [StringLength(100, ErrorMessage = "O Email deve possuir no máximo 100 caracteres")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento", Prompt = "Informe sua Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Foto de Perfil")]
        public IFormFile Foto { get; set; }

        [Display(Name = "Foto Atual")]
        public string FotoAtual { get; set; }

        [Display(Name = "Nome de Usuário")]
        public string UserName { get; set; }
    }

    public class AlterarSenhaVM
    {
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual", Prompt = "Informe sua Senha Atual")]
        [Required(ErrorMessage = "Por favor, informe sua Senha Atual")]
        public string SenhaAtual { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha", Prompt = "Informe sua Nova Senha")]
        [Required(ErrorMessage = "Por favor, informe sua Nova Senha")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A Senha deve possuir no mínimo 6 e no máximo 20 caracteres")]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha", Prompt = "Confirme sua Nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "As Senhas não Conferem.")]
        public string ConfirmarNovaSenha { get; set; }
    }
}

