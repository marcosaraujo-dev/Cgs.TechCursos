using Cgs.TechCursos.Domain.Entities;
using FluentValidation;

namespace Cgs.TechCursos.Domain.Validators
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        public ProfessorValidator()
        {
            Include(new PessoaValidator<Professor>());

            RuleFor(p => p.Disciplina)
                .NotEmpty().WithMessage("A disciplina é obrigatória.")
                .Length(2, 100).WithMessage("A disciplina deve ter entre 2 e 100 caracteres.");
        }
    }
}
