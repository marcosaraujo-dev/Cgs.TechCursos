using Cgs.TechCursos.Domain.Entities;
using FluentValidation;

namespace Cgs.TechCursos.Domain.Validators
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {
            RuleFor(professor => professor.Nome)
                .NotEmpty().WithMessage("O nome não pode ser vazio")
                .Length(2, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres");

            RuleFor(professor => professor.Email)
                .NotEmpty().WithMessage("Email não pode ser vazio")
                .EmailAddress().WithMessage("O email deve estar em um formato válido");
        }
    }
}
