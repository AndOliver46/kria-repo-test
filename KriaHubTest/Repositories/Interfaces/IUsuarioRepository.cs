using KriaHubTest.Models;

namespace KriaHubTest.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<UsuarioModel> GetAll();
        UsuarioModel GetById(int id);
        UsuarioModel? GetByEmail(string email);
        void Add(UsuarioModel usuario);
        void Update(UsuarioModel usuario);
        void Delete(int id);
    }
}
