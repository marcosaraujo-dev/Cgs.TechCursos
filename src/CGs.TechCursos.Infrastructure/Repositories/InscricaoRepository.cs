using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cgs.TechCursos.Infrastructure.Repositories
{
    public class InscricaoRepository : IInscricaoRepository
    {
        private readonly List<Inscricao> _inscricoes = new List<Inscricao>();

        public void Add(Inscricao inscricao)
        {
            _inscricoes.Add(inscricao);
        }

        public void Update(Inscricao inscricao)
        {
            var existeInscricao = GetById(inscricao.Id);
            if (existeInscricao != null)
            {        
               _inscricoes.Remove(existeInscricao);
                _inscricoes.Add(inscricao);
            }
        }

        public Inscricao GetById(Guid id)
        {
            return _inscricoes.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Inscricao> GetAll()
        {
            return _inscricoes;
        }

        public void Delete(Guid id)
        {
            var inscricao = GetById(id);
            if (inscricao != null)
            {
                _inscricoes.Remove(inscricao);
            }
        }
    }
}
