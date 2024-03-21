using KriaHubTest.Repositories.Interfaces;
using KriaHubTest.Services.Interfaces;

namespace KriaHubTest.Services
{
    public class UsuarioRepositorioService(IUsuarioRepositorioRepository usuariosRepositoriosRepository) : IUsuarioRepositorioService
    {
        private readonly IUsuarioRepositorioRepository _usuariosRepositoriosRepository = usuariosRepositoriosRepository;

        public void FavoritarRepositorio(int usuarioId, int repositorioId)
        {
            _usuariosRepositoriosRepository.AdicionarFavorito(usuarioId, repositorioId);
        }

        public IEnumerable<int> GetRepositoriosFavoritados(int usuarioId)
        {
            return _usuariosRepositoriosRepository.GetRepositoriosFavoritadosPorUsuario(usuarioId);
        }

        public bool IsRepositorioFavoritado(int usuarioId, int repositorioId)
        {
            return _usuariosRepositoriosRepository.IsRepositorioFavoritado(usuarioId, repositorioId);
        }

        public void RemoverFavorito(int usuarioId, int repositorioId)
        {
            _usuariosRepositoriosRepository.RemoverFavorito(usuarioId, repositorioId);
        }
    }
}
