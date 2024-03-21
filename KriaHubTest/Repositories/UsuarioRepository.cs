using KriaHubTest.Data;
using KriaHubTest.Models;
using KriaHubTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KriaHubTest.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbContext _context;

        public UsuarioRepository(BancoContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioModel> GetAll()
        {
            return _context.Set<UsuarioModel>().ToList();
        }

        public UsuarioModel? GetById(int id)
        {
            return _context.Set<UsuarioModel>().Find(id);
        }

        public void Add(UsuarioModel usuario)
        {
            _context.Set<UsuarioModel>().Add(usuario);
            _context.SaveChanges();
        }

        public void Update(UsuarioModel usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var usuario = _context.Set<UsuarioModel>().Find(id);
            if (usuario != null) 
            {
                _context.Set<UsuarioModel>().Remove(usuario);
                _context.SaveChanges();
            }
        }

        public UsuarioModel? GetByEmail(string email)
        {
            return _context.Set<UsuarioModel>()
                .FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
