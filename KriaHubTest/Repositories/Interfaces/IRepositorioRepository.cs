using KriaHubTest.Models;

namespace KriaHubTest.Repositories.Interfaces
{
    public interface IRepositorioRepository
    {
        IEnumerable<RepositorioModel> GetAll();
        IEnumerable<RepositorioModel> GetAllByUserId(int id);
        IEnumerable<RepositorioModel> GetAllPublic();
        RepositorioModel? GetById(int id);
        void Add(RepositorioModel repositorio);
        void Update(RepositorioModel repositorio);
        void Delete(int id);
        IEnumerable<RepositorioModel> GetAllInList(List<int> ids);

    }
}
