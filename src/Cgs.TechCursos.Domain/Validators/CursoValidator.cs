using Cgs.TechCursos.Domain.Entities;
using FluentValidation;
using System;

namespace Cgs.TechCursos.Domain.Validators
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        public CursoValidator()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("O título do curso é obrigatório.")
                .MinimumLength(5).WithMessage("O título do curso deve ter no mínimo 5 caracteres.")
                .MaximumLength(100).WithMessage("O título do curso deve ter no máximo 100 caracteres.");

            RuleFor(c => c.Descricao)
                .MaximumLength(500).WithMessage("A descrição do curso deve ter no máximo 500 caracteres.");

            RuleFor(c => c.TempoDuracao)
                .GreaterThan(0).WithMessage("O tempo de duração do curso deve ser maior que zero.");

            RuleFor(c => c.NumeroVagas)
                .GreaterThan(0).WithMessage("O número de vagas do curso deve ser maior que zero.");

            RuleFor(c => c.DataInicio)
                .Must(BeAValidDate).WithMessage("A data de início é obrigatória.")
                .LessThan(c => c.DataTermino).WithMessage("A data de início deve ser anterior à data de término.")
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de início deve ser maior ou igual à data atual.");

            RuleFor(c => c.DataTermino)
                .Must(BeAValidDate).WithMessage("A data de término é obrigatória.")
                .GreaterThan(c => c.DataInicio).WithMessage("A data de término deve ser posterior à data de início.");

            RuleFor(c => c.ProfessorId)
                .NotEmpty().WithMessage("O professor responsável pelo curso é obrigatório.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default(DateTime);
        }
    }
}
