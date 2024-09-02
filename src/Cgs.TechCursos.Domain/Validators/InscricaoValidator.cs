using Cgs.TechCursos.Domain.Entities;
using FluentValidation;
using System;


namespace Cgs.TechCursos.Domain.Validators
{
    public class InscricaoValidator : AbstractValidator<Inscricao>
    {
        public InscricaoValidator()
        {
            RuleFor(e => e.AlunoId)
                .NotEmpty().WithMessage("O ID do aluno é obrigatório.")
                .Must(id => id != Guid.Empty).WithMessage("O ID do aluno deve ser válido.");

            RuleFor(e => e.CursoId)
                .NotEmpty().WithMessage("O ID do curso é obrigatório.")
                .Must(id => id != Guid.Empty).WithMessage("O ID do curso deve ser válido.");

            RuleFor(e => e.DataInscricao)
                .NotEmpty().WithMessage("A data de inscrição é obrigatória.");

            RuleFor(e => e.Status)
                .IsInEnum().WithMessage("O status da inscrição deve ser um valor válido.");

           
        }
       
    }
}
