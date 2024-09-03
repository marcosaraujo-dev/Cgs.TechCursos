using Cgs.TechCursos.Domain.Validators;
using Cgs.TechCursos.Domain.Entities;
using Xunit;
using System;

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
            var Aluno = new Aluno("Jane Smith", "jane.smith@example.com", "12312312390", new DateTime(1995, 10, 20));

            var result = _validator.Validate(Aluno);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Not_Validate_Aluno_With_Empty_Nome()
        {
            var Aluno = new Aluno("", "marcos.araujo@cgs.com", "12312312390", new DateTime(1995, 10, 20));

            var result = _validator.Validate(Aluno);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
        }

        [Fact]
        public void Should_Not_Validate_Aluno_With_Invalid_Email()
        {
            var Aluno = new Aluno("Marcos Araujo", "invalid-email", "12312312390", new DateTime(1995, 10, 20));

            var result = _validator.Validate(Aluno);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Should_Not_Validate_Aluno_With_Invalid_Documento()
        {
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12345", new DateTime(1995, 10, 20));

            var result = _validator.Validate(aluno);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Documento");
        }

        [Fact]
        public void Should_Not_Validate_Aluno_With_Future_Date_Nascimento()
        {
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", DateTime.Now.AddYears(1));

            var result = _validator.Validate(aluno);

            Assert.False(result.IsValid);
            // Assuming that future dates are invalid; adjust if you have specific rules
            Assert.Contains(result.Errors, e => e.PropertyName == "DataNascimento");
        }

        [Fact]
        public void Should_Validate_Aluno_With_Valid_Matricula()
        {
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", new DateTime(1995, 10, 20));

            // Testing matricula format, assuming that it should be in a specific format
            Assert.Matches(@"^\d{9}$", aluno.Matricula);
        }



    }
}
