using System.ComponentModel.DataAnnotations;

namespace KriaHubTest.Models
{
    public class CadastroUsuarioDTO
    {
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuário")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 50 caracteres.")]
        public required string Senha { get; set; }
    }
}
