using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;


namespace Cgs.TechCursos.Domain.Contracts
{
    public interface IInscricaoRepository
    {
        void Add(Inscricao inscricao);
        void Update(Inscricao inscricao);
        Inscricao GetById(Guid id);
        IEnumerable<Inscricao> GetAll();

        void Delete(Guid id);
    }
}
