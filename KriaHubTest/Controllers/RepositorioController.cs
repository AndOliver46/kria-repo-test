using KriaHubTest.Models;
using KriaHubTest.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KriaHubTest.Controllers
{
    [Authorize]
    public class RepositorioController : Controller
    {
        private readonly IRepositorioService _repositorioService;

        public RepositorioController(IRepositorioService repositorioService)
        {
            _repositorioService = repositorioService ?? throw new ArgumentNullException(nameof(repositorioService));
        }

        public IActionResult CriarRepositorio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarRepositorio(CadastroRepositorioDTO repositorioDTO)
        {
            if (ModelState.IsValid)
            {
                int userId = GetUserId();

                _repositorioService.Criar(repositorioDTO, userId);

                TempData["MensagemSucesso"] = "Repositorio cadastrado com sucesso.";

                return RedirectToAction("MeusRepositorios");
            }

            return View(repositorioDTO);
        }

        public IActionResult ApagarRepositorio(int id)
        {
            RepositorioModel repositorio = _repositorioService.BuscarPorId(id);

            return View(repositorio);
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                int userId = GetUserId();

                _repositorioService.Deletar(id, userId);

                TempData["MensagemSucesso"] = "Repositorio excluido com sucesso.";

                return RedirectToAction("MeusRepositorios");
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Erro ao deletar repositorio. Error: {error.Message}";
                return RedirectToAction("MeusRepositorios");
            }
            
        }

        public IActionResult VerDetalhes(int id)
        {
            try
            {
                int userId = GetUserId();

                RepositorioDetalhesDTO repositorioModel = _repositorioService.VerDetalhes(id, userId);

                return View(repositorioModel);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Erro ao carregar detalhes. Error: {error.Message}";
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Favoritar(int id)
        {
            int userId = GetUserId();

            _repositorioService.Favoritar(id, userId);

            return RedirectToAction("VerDetalhes", new { id });
        }

        public IActionResult Desfavoritar(int id)
        {
            int userId = GetUserId();

            _repositorioService.Desfavoritar(id, userId);

            return RedirectToAction("VerDetalhes", new { id });
        }

        public IActionResult MeusRepositorios()
        {
            int userId = GetUserId();

            List<RepositorioModel> meusRepositorios = _repositorioService.BuscarPorUsuario(userId);

            TempData["Titulo"] = "Meus Repositórios";

            return View(meusRepositorios);
        }

        public IActionResult BuscarRepositorios()
        {
            List<RepositorioModel> repositoriosPublicos = _repositorioService.BuscarTodosPublicos();

            TempData["Titulo"] = "Buscar Repositórios";

            return View(repositoriosPublicos);
        }

        public IActionResult RepositoriosFavoritos()
        {
            int userId = GetUserId();

            List<RepositorioModel> repositoriosFavoritos = _repositorioService.MostrarFavoritos(userId);

            TempData["Titulo"] = "Repositórios Favoritos";

            return View("BuscarRepositorios", repositoriosFavoritos);
        }

        private int GetUserId()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                throw new InvalidOperationException("Usuário não autenticado ou ID inválido.");
            }

            return userId;
        }
    }
}
