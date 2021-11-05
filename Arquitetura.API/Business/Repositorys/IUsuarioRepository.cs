using Arquitetura.API.Business.Entities;
using Arquitetura.API.Models;

namespace Arquitetura.API.Business.Repositorys
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void commit();
        Usuario ObterUsuario(string login);
    }
}
