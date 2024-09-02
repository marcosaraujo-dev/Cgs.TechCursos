using Cgs.TechCursos.Domain.Entities;
using Cgs.TechCursos.Domain.Enums;
using Cgs.TechCursos.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cgs.TechCursos.Tests.Validators
{
    public class InscricaoValidatorTests
    {
        private readonly InscricaoValidator _validator;

        public InscricaoValidatorTests()
        {
            _validator = new InscricaoValidator();
        }

        [Fact]
        public void Should_Validate_Inscricao_With_Valid_Data()
        {
            var Inscricao = new Inscricao(Guid.NewGuid(), Guid.NewGuid(), InscricaoStatus.Pendente);

            var result = _validator.Validate(Inscricao);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Not_Validate_Inscricao_With_Empty_AlunoId()
        {
            var Inscricao = new Inscricao(Guid.Empty, Guid.NewGuid(), InscricaoStatus.Pendente);

            var result = _validator.Validate(Inscricao);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "AlunoId");
        }

        [Fact]
        public void Should_Not_Validate_Inscricao_With_Empty_CursoId()
        {
            var Inscricao = new Inscricao(Guid.NewGuid(), Guid.Empty, InscricaoStatus.Pendente);

            var result = _validator.Validate(Inscricao);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CursoId");
        }

        [Theory]
        [InlineData(InscricaoStatus.Pendente)]
        [InlineData(InscricaoStatus.Confirmada)]
        [InlineData(InscricaoStatus.Completa)]
        [InlineData(InscricaoStatus.Cancelada)]
        [InlineData(InscricaoStatus.Rejeitada)]
        public void Should_Validate_Inscricao_With_Valid_Status(InscricaoStatus status)
        {
            var Inscricao = new Inscricao(Guid.NewGuid(), Guid.NewGuid(), status);

            var result = _validator.Validate(Inscricao);

            Assert.True(result.IsValid);
        }
    }
}
