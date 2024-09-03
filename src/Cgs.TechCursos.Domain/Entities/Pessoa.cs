using System;

namespace Cgs.TechCursos.Domain.Entities
{
    public abstract class Pessoa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public string Documento { get; set; }
    }
}
