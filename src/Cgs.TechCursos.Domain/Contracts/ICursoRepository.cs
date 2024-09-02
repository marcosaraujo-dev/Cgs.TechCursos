using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;


namespace Cgs.TechCursos.Domain.Contracts
{
    public interface ICursoRepository
    {
        void Add(Curso professor);
        void Update(Curso professor);
        Curso GetById(Guid id);
        IEnumerable<Curso> GetAll();
        void Delete(Guid id);
    }
}
