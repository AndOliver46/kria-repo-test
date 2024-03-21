using KriaHubTest.Models;

namespace KriaHubTest.Services.Interfaces
{
    public interface IRepositorioService
    {
        List<RepositorioModel> BuscarTodos();
        List<RepositorioModel> BuscarTodosPublicos();
        List<RepositorioModel> BuscarPorUsuario(int id);
        RepositorioModel BuscarPorId(int id);
        void Criar(CadastroRepositorioDTO repositorio, int idUsuario);
        void Atualizar(RepositorioModel repositorio);
        void Deletar(int idRepositorio, int idUsuario);
        RepositorioDetalhesDTO VerDetalhes(int id, int userId);
        RepositorioDetalhesDTO Favoritar(int repoId, int userId);
        RepositorioDetalhesDTO Desfavoritar(int repoId, int userId);
        List<RepositorioModel> MostrarFavoritos(int userId);
    }
}
