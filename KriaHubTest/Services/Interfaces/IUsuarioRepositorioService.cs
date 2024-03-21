namespace KriaHubTest.Services.Interfaces
{
    public interface IUsuarioRepositorioService
    {
        void FavoritarRepositorio(int usuarioId, int repositorioId);
        void RemoverFavorito(int usuarioId, int repositorioId);
        IEnumerable<int> GetRepositoriosFavoritados(int usuarioId);
        bool IsRepositorioFavoritado(int usuarioId, int repositorioId);
    }
}
