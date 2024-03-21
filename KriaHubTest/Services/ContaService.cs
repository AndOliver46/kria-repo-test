using KriaHubTest.Models;
using KriaHubTest.Repositories.Interfaces;
using KriaHubTest.Services.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;

namespace KriaHubTest.Services
{
    public class ContaService : IContaService
    {
        private const int SaltSize = 32;
        private const int HashSize = 64;
        private const int Iterations = 10000;

        private readonly IUsuarioRepository _usuarioRepository;

        public ContaService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void CriarConta(CadastroUsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                throw new ArgumentNullException(nameof(usuarioDTO), "O objeto CadastroUsuarioDTO não pode ser nulo.");
            }

            UsuarioModel usuario = new UsuarioModel
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            };

            byte[] saltBytes = GerarSalt();

            usuario.Salt = Convert.ToBase64String(saltBytes);
            usuario.Senha = GerarSenhaHash(usuarioDTO.Senha, saltBytes);

            _usuarioRepository.Add(usuario);
        }

        public ClaimsPrincipal? AutenticarUsuario(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "O email não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrEmpty(senha))
            {
                throw new ArgumentNullException(nameof(senha), "A senha não pode ser nula ou vazia.");
            }

            UsuarioModel usuario = _usuarioRepository.GetByEmail(email);

            if (usuario == null || !VerificarSenhaHash(senha, usuario.Senha, usuario.Salt))
            {
                return null;
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");
            var principal = new ClaimsPrincipal(claimsIdentity);

            return principal;
        }

        private byte[] GerarSalt()
        {
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return saltBytes;
        }

        private string GerarSenhaHash(string senha, byte[] saltBytes)
        {
            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, saltBytes, Iterations))
            {
                hashBytes = pbkdf2.GetBytes(HashSize);
            }
            return Convert.ToBase64String(hashBytes);
        }

        private bool VerificarSenhaHash(string senha, string senhaHash, string salt)
        {
            byte[] hashBytes = Convert.FromBase64String(senhaHash);
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] testHash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, saltBytes, Iterations))
            {
                testHash = pbkdf2.GetBytes(HashSize);
            }

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i] != testHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
