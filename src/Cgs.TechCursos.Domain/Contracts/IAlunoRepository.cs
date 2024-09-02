using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;


namespace Cgs.TechCursos.Domain.Contracts
{
    public interface IAlunoRepository
    {
        void Add(Aluno professor);
        void Update(Aluno professor);
        Aluno GetById(Guid id);
        IEnumerable<Aluno> GetAll();
        void Delete(Guid id);
    }
}
