using Cgs.TechCursos.Application.Services;
using Cgs.TechCursos.Domain.Entities;
using Cgs.TechCursos.Domain.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cgs.TechCursos.Tests.Validators
{
    public class ProfessorValidatorTests
    {
        private readonly ProfessorValidator _validator;

        public ProfessorValidatorTests()
        {
            _validator = new ProfessorValidator();
        }

        [Fact]
        public void Should_Validate_Professor_With_Valid_Data()
        {
            var professor = new Professor("Marcos Araujo", "marcos.araso@cgs.com","12345678910", "Lógica");

            var result = _validator.Validate(professor);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Not_Validate_Professor_With_Empty_Name()
        {
            var professor = new Professor("", "marcos.araujo@cgs.com", "12345678910", "Lógica");

            var result = _validator.Validate(professor);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
        }

        [Fact]
        public void Should_Not_Validate_Professor_With_Invalid_Email()
        {
            var professor = new Professor("Marcos Araujo", "invalid-email", "12345678910", "Lógica");

            var result = _validator.Validate(professor);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        }
        [Fact]
        public void Should_Not_Validate_Professor_With_Empty_Disciplina()
        {
            var professor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com", "12345678910", "");

            var result = _validator.Validate(professor);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Disciplina");
        }

        [Fact]
        public void Should_Not_Validate_Professor_With_Invalid_Documento()
        {
            var professor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com", "123457434564534645", "Lógica");

            var result = _validator.Validate(professor);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Documento");
        }

    }
}
