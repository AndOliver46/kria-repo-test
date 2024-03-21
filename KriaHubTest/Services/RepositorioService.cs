using KriaHubTest.Enums;
using KriaHubTest.Models;
using KriaHubTest.Repositories.Interfaces;
using KriaHubTest.Services.Interfaces;

namespace KriaHubTest.Services
{
    public class RepositorioService : IRepositorioService
    {
        private readonly IRepositorioRepository _repositorioRepository;
        private readonly IUsuarioRepositorioService _usuariosRepositoriosService;

        public RepositorioService(
            IRepositorioRepository repositorioRepository, IUsuarioRepositorioService usuariosRepositoriosService)
        {
            _repositorioRepository = repositorioRepository;
            _usuariosRepositoriosService = usuariosRepositoriosService;
        }

        public void Atualizar(RepositorioModel repositorio)
        {
            if (repositorio == null)
            {
                throw new ArgumentNullException(nameof(repositorio), "O objeto RepositorioModel não pode ser nulo.");
            }

            _repositorioRepository.Update(repositorio);
        }

        public RepositorioModel BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do repositório é inválido.");
            }

            RepositorioModel? repositorioModel = _repositorioRepository.GetById(id);

            if(repositorioModel == null)
            {
                throw new Exception("Repositório não encontrado.");
            }

            return repositorioModel;
        }

        public List<RepositorioModel> BuscarPorUsuario(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do usuário é inválido.");
            }

            return _repositorioRepository.GetAllByUserId(id).ToList();
        }

        public List<RepositorioModel> BuscarTodos()
        {
            return _repositorioRepository.GetAll().ToList();
        }

        public List<RepositorioModel> BuscarTodosPublicos()
        {
            return _repositorioRepository.GetAllPublic().ToList();
        }

        public void Criar(CadastroRepositorioDTO repositorio, int idUsuario)
        {
            if (repositorio == null)
            {
                throw new ArgumentNullException(nameof(repositorio), "O objeto RepositorioModel não pode ser nulo.");
            }

            if (repositorio.UsuarioId != idUsuario)
            {
                throw new Exception("O usuário logado não pode cadastrar esse repositório.");
            }

            RepositorioModel repositorioModel = new RepositorioModel
            {
                Nome = repositorio.Nome,
                Descricao = repositorio.Descricao,
                Linguagem = repositorio.Linguagem,
                DataAtualizacao = repositorio.DataAtualizacao,
                Visibilidade = repositorio.Visibilidade,
                UsuarioId = repositorio.UsuarioId
            };

            _repositorioRepository.Add(repositorioModel);
        }

        public void Deletar(int idRepositorio, int idUsuario)
        {
            var repositorio = BuscarPorId(idRepositorio);

            if (repositorio == null)
            {
                throw new ArgumentOutOfRangeException(nameof(repositorio), "O repositório informado já foi deletado.");
            }

            if (repositorio.Usuario.Id != idUsuario)
            {
                throw new ArgumentOutOfRangeException(nameof(repositorio), "O repositório informado não pertence ao usuário logado.");
            }

            _repositorioRepository.Delete(idRepositorio);
        }

        public RepositorioDetalhesDTO VerDetalhes(int id, int userId)
        {

            RepositorioModel repositorioModel = BuscarPorId(id);

            if (repositorioModel.Visibilidade.Equals(VisibilidadeEnum.Privado) && repositorioModel.Usuario.Id != userId)
            {
                throw new Exception("Você não tem permissão apra ver detalhes desse repositório.");
            }

            RepositorioDetalhesDTO repositorioDetalhesModel = new RepositorioDetalhesDTO
            {
                Id = repositorioModel.Id,
                Nome = repositorioModel.Nome,
                Descricao = repositorioModel.Descricao,
                Linguagem = repositorioModel.Linguagem,
                DataAtualizacao = repositorioModel.DataAtualizacao,
                Visibilidade = repositorioModel.Visibilidade,
                UsuarioId = repositorioModel.UsuarioId,
                Usuario = repositorioModel.Usuario,
                Favorito = false // Defina o valor de Favorito conforme necessário
            };

            repositorioDetalhesModel.Favorito = _usuariosRepositoriosService.IsRepositorioFavoritado(userId, id);

            return repositorioDetalhesModel;
        }

        public RepositorioDetalhesDTO Favoritar(int repoId, int userId)
        {
            _usuariosRepositoriosService.FavoritarRepositorio(userId, repoId);

            return VerDetalhes(repoId, userId);
        }

        public RepositorioDetalhesDTO Desfavoritar(int repoId, int userId)
        {
            _usuariosRepositoriosService.RemoverFavorito(userId, repoId);

            return VerDetalhes(repoId, userId);
        }

        public List<RepositorioModel> MostrarFavoritos(int userId)
        {
            List<int> idsFavoritados = _usuariosRepositoriosService.GetRepositoriosFavoritados(userId).ToList();
            return _repositorioRepository.GetAllInList(idsFavoritados).ToList();
        }
    }
}
