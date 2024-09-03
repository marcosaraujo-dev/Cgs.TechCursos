using System;

namespace Cgs.TechCursos.Domain.Entities
{
    public class Aluno: Pessoa
    {
        public DateTime DataNascimento { get; set; }
        public string Matricula { get; private set; }

        public Aluno(string nome, string email, string documento, DateTime dataNascimento)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Documento = documento;
            DataNascimento = dataNascimento;
            Matricula = GerarMatricula();
        }

        private string GerarMatricula()
        {
        return $"{DateTime.Now:ddMMssfff}";
        }
    }
}
