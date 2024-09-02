using System;
using System.Linq;
using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using Cgs.TechCursos.Domain.Notifications;
using Cgs.TechCursos.Domain.Validators;

namespace Cgs.TechCursos.Application.Services
{

    public class ProfessorService : Notifiable
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public void Create(Professor professor)
        {
            if (professor == null)
            {
                AddNotification("Professor", "O professor não pode ser nulo.");
                return;
            }

            if (_professorRepository.GetAll().Any(p => p.Email == professor.Email))
            {
                AddNotification(nameof(professor.Email), "Já existe um professor cadastrado com esse email.");
                return;
            }
            var validator = new ProfessorValidator();
            var result = validator.Validate(professor);

            if (!result.IsValid)
            {
                AddNotifications(result.Errors.Select(e => new Notification(e.PropertyName, e.ErrorMessage)));
                return;
            }

            _professorRepository.Add(professor);
        }

        public void Update(Professor professor)
        {
            if (professor == null)
            {
                AddNotification("Professor", "O professor não pode ser nulo.");
                return;
            }

            if (_professorRepository.GetById(professor.Id) == null)
            {
                AddNotification(nameof(professor.Id), "O professor não foi localizado.");
                return;
            }

            var validator = new ProfessorValidator();
            var result = validator.Validate(professor);

            if (!result.IsValid)
            {
                AddNotifications(result.Errors.Select(e => new Notification(e.PropertyName, e.ErrorMessage)));
                return;
            }

            _professorRepository.Update(professor);
        }

        public Professor GetById(Guid id)
        {
            if(id == Guid.Empty)
            {
                AddNotification(nameof(id), "O Id do professor é inválido.");
                return null;
            }
            var professor = _professorRepository.GetById(id);
            if (professor == null)
                AddNotification("Professor", "Professor não localizado.");

            return professor;
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddNotification(nameof(id), "O Id do professor é inválido.");
                return;
            }

            var professor = _professorRepository.GetById(id);
            if (professor == null)
            {
                AddNotification("Professor", "O professor não foi encontrado.");
                return;
            }

            _professorRepository.Delete(id);
        }
    }

}
