namespace KriaHubTest.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string Salt { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public ICollection<RepositorioModel>? RepositoriosProprios { get; set; }
    }
}
