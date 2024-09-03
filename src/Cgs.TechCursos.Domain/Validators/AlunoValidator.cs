using Cgs.TechCursos.Domain.Entities;
using FluentValidation;
using System;

namespace Cgs.TechCursos.Domain.Validators
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {
            Include(new PessoaValidator<Aluno>());

            RuleFor(a => a.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser no passado.");
        }
    }
}
