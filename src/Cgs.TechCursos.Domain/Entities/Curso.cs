using System;
using System.Globalization;

namespace Cgs.TechCursos.Domain.Entities
{
    public class Curso
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public int TempoDuracao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTermino { get; private set; }
        public Guid ProfessorId { get; private set; }
        public int NumeroVagas { get; private set; }

        public Curso(string titulo, string descricao, int tempoDuracao, int numeroVagas, Guid professorId, DateTime dataInicio, DateTime dataTermino)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Descricao = descricao;
            TempoDuracao = tempoDuracao;
            NumeroVagas = numeroVagas;
            ProfessorId = professorId;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
        }
    }
}
