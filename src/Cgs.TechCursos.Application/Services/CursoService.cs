using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using Cgs.TechCursos.Domain.Notifications;
using Cgs.TechCursos.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgs.TechCursos.Application.Services
{
    public class CursoService : Notifiable
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void Create(Curso curso)
        {
            var validator = new CursoValidator();
            var result = validator.Validate(curso);

            if (!result.IsValid)
            {
                AddNotifications(result.Errors.Select(e => new Notification(e.PropertyName, e.ErrorMessage)));
                return;
            }

            _cursoRepository.Add(curso);
        }

        public void Update(Curso curso)
        {
            if (curso == null)
            {
                AddNotification("Aluno", "O curso não pode ser nulo.");
                return;
            }

            if (_cursoRepository.GetById(curso.Id) == null)
            {
                AddNotification(nameof(curso.Id), "O curso não foi localizado.");
                return;
            }

            var validator = new CursoValidator();
            var result = validator.Validate(curso);

            if (!result.IsValid)
            {
                AddNotifications(result.Errors.Select(e => new Notification(e.PropertyName, e.ErrorMessage)));
                return;
            }

            _cursoRepository.Update(curso);
        }

        public Curso Get(Guid id)
        {
            var student = _cursoRepository.GetById(id);
            if (student == null)
                AddNotification("curso", "curso não localizado");

            return student;
        }
        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddNotification(nameof(id), "O Id do aluno é inválido.");
                return;
            }

            var curso = _cursoRepository.GetById(id);
            if (curso == null)
            {
                AddNotification("Curso", "O curso não foi encontrado.");
                return;
            }

            _cursoRepository.Delete(id);
        }
    }
}
