namespace KriaHubTest.Repositories.Interfaces
{
    public interface IUsuarioRepositorioRepository
    {
        void AdicionarFavorito(int usuarioId, int repositorioId);
        void RemoverFavorito(int usuarioId, int repositorioId);
        IEnumerable<int> GetRepositoriosFavoritadosPorUsuario(int usuarioId);
        bool IsRepositorioFavoritado(int usuarioId, int repositorioId);
    }
}
