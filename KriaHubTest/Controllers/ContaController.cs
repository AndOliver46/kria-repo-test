using KriaHubTest.Models;
using KriaHubTest.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KriaHubTest.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            ClaimsPrincipal principal = _contaService.AutenticarUsuario(email, password);

            if (principal == null)
            {
                TempData["MensagemErro"] = "Usuário ou senha inválidos";
                return View();
            }

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Conta");
        }

        public IActionResult CriarConta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarConta(CadastroUsuarioDTO usuarioDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contaService.CriarConta(usuarioDTO);

                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Login");
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    TempData["MensagemErro"] = "O email informado já está cadastrado.";
                    return RedirectToAction("CriarConta", usuarioDTO);
                }

                TempData["MensagemErro"] = "Erro ao cadastrar usuário.";
                return RedirectToAction("CriarConta", usuarioDTO);
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao cadastrar usuário.";
                return RedirectToAction("CriarConta", usuarioDTO);
            }

            return View(usuarioDTO);
        }
    }
}
