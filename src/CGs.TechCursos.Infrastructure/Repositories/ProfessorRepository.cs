using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cgs.TechCursos.Infrastructure.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly List<Professor> _professores = new List<Professor>();

        public void Add(Professor professor)
        {
            _professores.Add(professor);
        }

        public void Update(Professor professor)
        {
            var existeProfessor = GetById(professor.Id);
            if (existeProfessor != null)
            {
                _professores.Remove(existeProfessor); 
                _professores.Add(professor);          
            }
        }

        public Professor GetById(Guid id)
        {
            return _professores.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Professor> GetAll()
        {
            return _professores;
        }

        public void Delete(Guid id)
        {
            var professor = GetById(id);
            if (professor != null)
            {
                _professores.Remove(professor);
            }
        }
    }
}
