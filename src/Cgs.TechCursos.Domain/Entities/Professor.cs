using System;

namespace Cgs.TechCursos.Domain.Entities
{
    public class Professor
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public Professor(string nome, string email)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
        }
    }
}
