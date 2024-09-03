using System;
using System.Globalization;

namespace Cgs.TechCursos.Domain.Entities
{
    public class Professor: Pessoa
    {
      public string Disciplina { get; set; }
        public Professor(string nome, string email, string documento, string disciplina)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Documento = documento;
            Disciplina = disciplina;
        }
    }
}
