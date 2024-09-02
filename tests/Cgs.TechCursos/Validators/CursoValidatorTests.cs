using Cgs.TechCursos.Domain.Entities;
using Cgs.TechCursos.Domain.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cgs.TechCursos.Tests.Validators
{
    public class CursoValidatorTests
    {
        private readonly CursoValidator _validator;

        public CursoValidatorTests()
        {
            _validator = new CursoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Titulo_Is_Empty()
        {
            var curso = new Curso("", "Descrição de curso válida", 10, 30, Guid.NewGuid(), DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var result = _validator.TestValidate(curso);
            result.ShouldHaveValidationErrorFor(c => c.Titulo);
        }

        [Fact]
        public void Should_Have_Error_When_Titulo_Is_Shorter_Than_5_Characters()
        {
            var curso = new Curso("C#", "Curso de Testes C#", 10, 30, Guid.NewGuid(), DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var result = _validator.TestValidate(curso);
            result.ShouldHaveValidationErrorFor(c => c.Titulo);
        }

        [Fact]
        public void Should_Have_Error_When_TempoDuracao_Is_Zero_Or_Less()
        {
            var curso = new Curso("Treinamento Testes C#", "Curso de Testes C#", 0, 30, Guid.NewGuid(), DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var result = _validator.TestValidate(curso);
            result.ShouldHaveValidationErrorFor(c => c.TempoDuracao);
        }

        [Fact]
        public void Should_Have_Error_When_NumeroVagas_Is_Zero_Or_Less()
        {
            var curso = new Curso("Treinamento Testes C#", "Curso de Testes C#", 10, 0, Guid.NewGuid(), DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var result = _validator.TestValidate(curso);
            result.ShouldHaveValidationErrorFor(c => c.NumeroVagas);
        }

        [Fact]
        public void Should_Have_Error_When_DataInicio_Is_Default()
        {
            var curso = new Curso("Treinamento Testes C#", "Curso de Testes C#", 10, 30, Guid.NewGuid(), default(DateTime), DateTime.UtcNow.AddDays(10));
            var result = _validator.TestValidate(curso);
            result.ShouldHaveValidationErrorFor(c => c.DataInicio);
        }

        [Fact]
        public void Should_Have_Error_When_DataInicio_Is_After_DataTermino()
        {
            var curso = new Curso("Treinamento Testes C#", "Curso de Testes C#", 10, 30, Guid.NewGuid(), DateTime.UtcNow.AddDays(10), DateTime.UtcNow.AddDays(1));
            var result = _validator.TestValidate(curso);
            result.ShouldHaveValidationErrorFor(c => c.DataInicio);
        }

        [Fact]
        public void Should_Have_Error_When_ProfessorId_Is_Empty()
        {
            var curso = new Curso("Treinamento Testes C#", "Curso de Testes C#", 10, 30, Guid.Empty, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var result = _validator.TestValidate(curso);
            result.ShouldHaveValidationErrorFor(c => c.ProfessorId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Curso_Is_Valid()
        {
            var curso = new Curso("Treinamento Testes C#", "Curso de Testes C#", 10, 30, Guid.NewGuid(), DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var result = _validator.TestValidate(curso);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
