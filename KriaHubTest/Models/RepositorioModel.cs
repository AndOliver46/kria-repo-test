using KriaHubTest.Enums;

namespace KriaHubTest.Models
{
    public class RepositorioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public LinguagemEnum Linguagem { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public VisibilidadeEnum Visibilidade { get; set;}

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
