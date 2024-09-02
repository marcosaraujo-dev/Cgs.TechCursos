using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cgs.TechCursos.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly List<Curso> _cursos = new List<Curso>();

        public void Add(Curso Curso)
        {
            _cursos.Add(Curso);
        }

        public void Update(Curso Curso)
        {
            var existeCurso = GetById(Curso.Id);
            if (existeCurso != null)
            {
                existeCurso = Curso;
            }
        }

        public Curso GetById(Guid id)
        {
            return _cursos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Curso> GetAll()
        {
            return _cursos;
        }

        public void Delete(Guid id)
        {
            var curso = GetById(id);
            if (curso != null)
            {
                _cursos.Remove(curso);
            }
        }
    }
}
