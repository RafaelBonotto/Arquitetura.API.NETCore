using Arquitetura.API.Business.Entities;
using Arquitetura.API.Business.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arquitetura.API.Infraestruture.Data.Repositorys
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _repository;

        public CursoRepository(CursoDbContext repository)
        {
            _repository = repository;
        }

        public void Adicionar(Curso curso)
        {
            _repository.Curso.Add(curso);    
        }

        public void Commit()
        {
            _repository.SaveChanges();
        }

        public IList<Curso> ObterCursosPorUsuario(int codigoUsuario)
        {
            var cursos = new List<Curso>();
            try
            {
                cursos = _repository.Curso.Include(x => x.Usuario).Where(x => x.CodigoUsuario == codigoUsuario).ToList();

                if (cursos is null || cursos.Count == 0) return null;

                return cursos;
            }
            catch (Exception error)
            {
                throw error;
            }
            

        }
    }
}
