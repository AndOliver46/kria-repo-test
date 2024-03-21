using KriaHubTest.Data;
using KriaHubTest.Models;
using KriaHubTest.Repositories.Interfaces;

namespace KriaHubTest.Repositories
{
    public class UsuarioRepositorioRepository : IUsuarioRepositorioRepository
    {
        private readonly BancoContext _context;

        public UsuarioRepositorioRepository(BancoContext context)
        {
            _context = context;
        }

        public void AdicionarFavorito(int usuarioId, int repositorioId)
        {
            // Verifique se o repositório já está favoritado
            if (!IsRepositorioFavoritado(usuarioId, repositorioId))
            {
                var associacao = new UsuarioRepositorioModel
                {
                    UsuarioId = usuarioId,
                    RepositorioId = repositorioId
                };

                _context.UsuariosRepositorios.Add(associacao);
                _context.SaveChanges();
            }
        }

        public void RemoverFavorito(int usuarioId, int repositorioId)
        {
            var associacao = _context.UsuariosRepositorios
                .FirstOrDefault(ur => ur.UsuarioId == usuarioId && ur.RepositorioId == repositorioId);

            if (associacao != null)
            {
                _context.UsuariosRepositorios.Remove(associacao);
                _context.SaveChanges();
            }
        }

        public IEnumerable<int> GetRepositoriosFavoritadosPorUsuario(int usuarioId)
        {
            return _context.UsuariosRepositorios
                .Where(ur => ur.UsuarioId == usuarioId)
                .Select(ur => ur.RepositorioId);
        }

        public bool IsRepositorioFavoritado(int usuarioId, int repositorioId)
        {
            return _context.UsuariosRepositorios
                .Any(ur => ur.UsuarioId == usuarioId && ur.RepositorioId == repositorioId);
        }
    }

}
