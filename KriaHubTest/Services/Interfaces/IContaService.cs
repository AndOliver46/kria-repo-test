using KriaHubTest.Models;
using System.Security.Claims;

namespace KriaHubTest.Services.Interfaces
{
    public interface IContaService
    {
        ClaimsPrincipal? AutenticarUsuario(string email, string senha);
        void CriarConta(CadastroUsuarioDTO usuarioDTO);
    }
}
