using System;

namespace Cgs.TechCursos.Domain.Entities
{
    public class Aluno
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public string Documento { get; private set; }

        public Aluno(string nome, string email)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
        }
    }
}
