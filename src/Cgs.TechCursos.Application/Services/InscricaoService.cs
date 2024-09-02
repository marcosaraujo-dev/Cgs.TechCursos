

using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using Cgs.TechCursos.Domain.Notifications;
using Cgs.TechCursos.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cgs.TechCursos.Application.Services
{
    public class InscricaoService : Notifiable
    {
        private readonly IInscricaoRepository _inscricaoRepository;

        public InscricaoService(IInscricaoRepository inscricaoRepository)
        {
            _inscricaoRepository = inscricaoRepository;
        }

        public bool Create(Inscricao inscricao)
        {
            if (inscricao == null)
            {
                AddNotification("Inscrição", "Inscrição não pode ser nula.");
                return false;
            }

            if (_inscricaoRepository.GetAll().Any(i => i.AlunoId == inscricao.AlunoId && i.CursoId == inscricao.CursoId))
            {
                AddNotification("Inscrição", "O aluno já está inscrito neste curso.");
                return false;
            }

            _inscricaoRepository.Add(inscricao);
            return true;
        }

        public bool Update(Inscricao inscricao)
        {
            if (inscricao == null)
            {
                AddNotification("Inscrição", "Inscrição não pode ser nula.");
                return false;
            }

            var existingInscricao = _inscricaoRepository.GetById(inscricao.Id);
            if (existingInscricao == null)
            {
                AddNotification("Inscrição", "Inscrição não encontrada.");
                return false;
            }

            _inscricaoRepository.Update(inscricao);
            return true;
        }

        public Inscricao GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddNotification("Id", "Id da inscrição inválido.");
                return null;
            }

            var inscricao = _inscricaoRepository.GetById(id);
            if (inscricao == null)
            {
                AddNotification("Inscrição", "Inscrição não encontrada.");
                return null;
            }

            return inscricao;
        }

        public IEnumerable<Inscricao> GetAll()
        {
            return _inscricaoRepository.GetAll();
        }

        public bool Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddNotification("Id", "Id da inscrição inválido.");
                return false;
            }

            var inscricao = _inscricaoRepository.GetById(id);
            if (inscricao == null)
            {
                AddNotification("Inscrição", "Inscrição não encontrada.");
                return false;
            }

            _inscricaoRepository.Delete(id);
            return true;
        }
    }
}
