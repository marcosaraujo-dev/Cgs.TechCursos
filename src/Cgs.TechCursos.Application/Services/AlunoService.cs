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
    public class AlunoService : Notifiable
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public void Create(Aluno aluno)
        {
            var validator = new AlunoValidator();
            var result = validator.Validate(aluno);

            if (!result.IsValid)
            {
                AddNotifications(result.Errors.Select(e => new Notification(e.PropertyName, e.ErrorMessage)));
                return;
            }

            _alunoRepository.Add(aluno);
        }

        public void Update(Aluno aluno)
        {
            if (aluno == null)
            {
                AddNotification("Aluno", "O aluno não pode ser nulo.");
                return;
            }

            if (_alunoRepository.GetById(aluno.Id) == null)
            {
                AddNotification(nameof(aluno.Id), "O aluno não foi localizado.");
                return;
            }

            var validator = new AlunoValidator();
            var result = validator.Validate(aluno);

            if (!result.IsValid)
            {
                AddNotifications(result.Errors.Select(e => new Notification(e.PropertyName, e.ErrorMessage)));
                return;
            }

            _alunoRepository.Update(aluno);
        }

        public Aluno Get(Guid id)
        {
            var student = _alunoRepository.GetById(id);
            if (student == null)
                AddNotification("Aluno", "Aluno não localizado");

            return student;
        }
        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddNotification(nameof(id), "O Id do aluno é inválido.");
                return;
            }

            var aluno = _alunoRepository.GetById(id);
            if (aluno == null)
            {
                AddNotification("Aluno", "O aluno não foi encontrado.");
                return;
            }

            _alunoRepository.Delete(id);
        }
    }
}
