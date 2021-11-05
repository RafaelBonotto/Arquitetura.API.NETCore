using Arquitetura.API.Business.Entities;
using Arquitetura.API.Business.Repositorys;
using Arquitetura.API.Models;
using System.Linq;

namespace Arquitetura.API.Infraestruture.Data.Repositorys
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _context;

        public UsuarioRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
        }

        public void commit()
        {
            _context.SaveChanges();
        }

        public Usuario ObterUsuario(string login)
        {
            var usuarioBanco = new Usuario();
            try
            {
                usuarioBanco = _context.Usuario.Where(x => x.Login.Equals(login)).FirstOrDefault();

                if (usuarioBanco is null) return null;

                return usuarioBanco;
            }
            catch (System.Exception error)
            {
                throw error;
            }
        }
    }
}
