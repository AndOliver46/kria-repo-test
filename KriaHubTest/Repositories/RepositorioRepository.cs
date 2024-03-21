using KriaHubTest.Data;
using KriaHubTest.Enums;
using KriaHubTest.Models;
using KriaHubTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KriaHubTest.Repositories
{
    public class RepositorioRepository : IRepositorioRepository
    {
        private readonly DbContext _context;

        public RepositorioRepository(BancoContext context)
        {
            _context = context;
        }

        public IEnumerable<RepositorioModel> GetAll()
        {
            return _context.Set<RepositorioModel>().ToList();
        }

        public RepositorioModel? GetById(int id)
        {
            return _context.Set<RepositorioModel>()
                .Include(r => r.Usuario)
                .SingleOrDefault(r => r.Id == id);
        }

        public void Add(RepositorioModel repositorio)
        {
            _context.Set<RepositorioModel>().Add(repositorio);
            _context.SaveChanges();
        }

        public void Update(RepositorioModel repositorio)
        {
            _context.Entry(repositorio).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var repositorio = _context.Set<RepositorioModel>().Find(id);
            if (repositorio != null) {
                _context.Set<RepositorioModel>().Remove(repositorio);
                _context.SaveChanges();
            }
        }

        public IEnumerable<RepositorioModel> GetAllByUserId(int id)
        {
            return _context.Set<RepositorioModel>()
                .Where(r => r.UsuarioId == id);
        }

        public IEnumerable<RepositorioModel> GetAllPublic()
        {
            return _context.Set<RepositorioModel>()
                .Include(r => r.Usuario)
                .Where(r => r.Visibilidade == VisibilidadeEnum.Publico);
        }

        public IEnumerable<RepositorioModel> GetAllInList(List<int> ids)
        {
            return _context.Set<RepositorioModel>()
                .Include(r => r.Usuario)
                .Where(r => ids.Contains(r.Id));
        }
    }
}
