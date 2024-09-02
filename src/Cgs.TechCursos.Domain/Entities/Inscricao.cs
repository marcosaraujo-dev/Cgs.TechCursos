using Cgs.TechCursos.Domain.Enums;
using System;

namespace Cgs.TechCursos.Domain.Entities
{
    public class Inscricao
    {
        public Guid Id { get; private set; }
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public DateTime DataInscricao { get; private set; }
        public InscricaoStatus Status { get; private set; }

        public Inscricao(Guid alunoId, Guid cursoId, InscricaoStatus inscricaoStatus)
        {
            Id = Guid.NewGuid();
            AlunoId = alunoId;
            CursoId = cursoId;
            Status = inscricaoStatus;
            DataInscricao = DateTime.UtcNow;
        }
    }
}
