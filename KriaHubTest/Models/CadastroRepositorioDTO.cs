using KriaHubTest.Enums;
using System.ComponentModel.DataAnnotations;

namespace KriaHubTest.Models
{
    public class CadastroRepositorioDTO
    {
        [Required(ErrorMessage = "O nome do repositório é obrigatório.")]
        public string Nome { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "A descrição deve ter entre 5 e 100 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A linguagem do repositório é obrigatória.")]
        public LinguagemEnum Linguagem { get; set; }

        [Display(Name = "Data de Atualização")]
        [DataType(DataType.Date)]
        public DateTime DataAtualizacao { get; set; }

        [Required(ErrorMessage = "A visibilidade do repositório é obrigatória.")]
        public VisibilidadeEnum Visibilidade { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }
    }
}
