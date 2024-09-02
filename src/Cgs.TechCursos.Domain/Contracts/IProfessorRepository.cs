using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;


namespace Cgs.TechCursos.Domain.Contracts
{
    public interface IProfessorRepository
    {
        void Add(Professor professor);
        void Update(Professor professor);
        Professor GetById(Guid id);
        IEnumerable<Professor> GetAll();

        void Delete(Guid id);
    }


}
