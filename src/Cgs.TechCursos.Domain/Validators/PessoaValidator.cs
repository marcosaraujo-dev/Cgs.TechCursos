using Cgs.TechCursos.Domain.Entities;
using FluentValidation;


namespace Cgs.TechCursos.Domain.Validators
{
    public class PessoaValidator<T> : AbstractValidator<T> where T : Pessoa
    {   
        public PessoaValidator()
        {
            RuleFor(pessoa => pessoa.Nome)
                .NotEmpty().WithMessage("O nome não pode ser vazio.")
                .Length(2, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            RuleFor(p => p.Documento)
                .NotEmpty().WithMessage("O documento é obrigatório.")
                 .Must(d => d.Length == 11 || d.Length == 14).WithMessage("O documento deve ter 11 ou 14 caracteres.");


            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser válido.");
        }
    }
}
