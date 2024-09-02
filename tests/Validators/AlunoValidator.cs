using Cgs.TechCursos.Domain.Validators;
using Cgs.TechCursos.Domain.Entities;
using Xunit;

namespace Cgs.TechCursos.Tests.Validators
{
    public class AlunoValidatorTests
    {
        private readonly AlunoValidator _validator;

        public AlunoValidatorTests()
        {
            _validator = new AlunoValidator();
        }

        [Fact]
        public void Should_Validate_Aluno_With_Valid_Data()
        {
            var Aluno = new Aluno("Jane Smith", "jane.smith@example.com");

            var result = _validator.Validate(Aluno);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Not_Validate_Aluno_With_Empty_Nome()
        {
            var Aluno = new Aluno("", "jane.smith@example.com");

            var result = _validator.Validate(Aluno);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
        }

        [Fact]
        public void Should_Not_Validate_Aluno_With_Invalid_Email()
        {
            var Aluno = new Aluno("Jane Smith", "invalid-email");

            var result = _validator.Validate(Aluno);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }
    }
}
