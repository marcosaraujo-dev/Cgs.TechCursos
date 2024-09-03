using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cgs.TechCursos.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly List<Aluno> _alunos = new List<Aluno>();

        public void Add(Aluno aluno)
        {
            _alunos.Add(aluno);
        }

        public void Update(Aluno aluno)
        {
            var existeAluno = GetById(aluno.Id);
            if (existeAluno != null)
            {
                _alunos.Remove(existeAluno);
                _alunos.Add(aluno);
            }

        }

        public Aluno GetById(Guid id)
        {
            return _alunos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Aluno> GetAll()
        {
            return _alunos;
        }

        public void Delete(Guid id)
        {
            var aluno = GetById(id);
            if (aluno != null)
            {
                _alunos.Remove(aluno);
            }
        }
    }
}
