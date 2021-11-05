using Arquitetura.API.Business.Entities;
using System.Collections.Generic;

namespace Arquitetura.API.Business.Repositorys
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();
        IList<Curso> ObterCursosPorUsuario(int codigoUsuario);
    }
}
