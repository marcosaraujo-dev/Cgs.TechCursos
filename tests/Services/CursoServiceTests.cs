using Cgs.TechCursos.Application.Services;
using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using Moq;
using System;
using Xunit;

namespace Cgs.TechCursos.Tests.Services
{
    public class CursoServiceTests
    {
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly CursoService _cursoService;

        public CursoServiceTests()
        {
            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _cursoService = new CursoService(_cursoRepositoryMock.Object);
        }

        [Fact]
        public void Create_Should_AddCurso_When_CursoIsValid()
        {
            // Arrange
            var curso = new Curso("Treinamento C#", "Treinamento de Testes C#", 40, 30, Guid.NewGuid(), DateTime.Now.AddDays(1), DateTime.Now.AddMonths(3));

            // Act
            _cursoService.Create(curso);

            // Assert
            Assert.True(_cursoService.IsValid);
            _cursoRepositoryMock.Verify(repo => repo.Add(curso), Times.Once);
        }

        [Fact]
        public void Create_Should_AddNotification_When_CursoIsInvalid()
        {
            // Arrange
            var curso = new Curso("", "", 0, 0, Guid.Empty, DateTime.Now, DateTime.Now.AddMonths(3)); // Dados inválidos

            // Act
            _cursoService.Create(curso);

            // Assert
            Assert.False(_cursoService.IsValid);
            Assert.Contains(_cursoService.Notifications, n => n.Property == "Titulo" && n.Message == "O título do curso é obrigatório.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "Titulo" && n.Message == "O título do curso deve ter no mínimo 5 caracteres.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "TempoDuracao" && n.Message == "O tempo de duração do curso deve ser maior que zero.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "NumeroVagas" && n.Message == "O número de vagas do curso deve ser maior que zero.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "DataInicio" && n.Message == "A data de início deve ser maior ou igual à data atual.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "ProfessorId" && n.Message == "O professor responsável pelo curso é obrigatório.");

            _cursoRepositoryMock.Verify(repo => repo.Add(It.IsAny<Curso>()), Times.Never);
        }

        [Fact]
        public void Update_Should_UpdateCurso_When_CursoIsValid()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            var curso = new Curso("Treinamento C#", "Treinamento de Testes C#", 60, 25, Guid.NewGuid(), DateTime.Now.AddDays(1), DateTime.Now.AddMonths(3));
            typeof(Curso).GetProperty(nameof(Curso.Id)).SetValue(curso, cursoId);

            _cursoRepositoryMock.Setup(repo => repo.GetById(cursoId)).Returns(curso);

            // Act
            _cursoService.Update(curso);

            // Assert
            Assert.True(_cursoService.IsValid);
            _cursoRepositoryMock.Verify(repo => repo.Update(curso), Times.Once);
        }

        [Fact]
        public void Update_Should_AddNotification_When_CursoIsNull()
        {
            // Act
            _cursoService.Update(null);

            // Assert
            Assert.False(_cursoService.IsValid);
            Assert.Contains(_cursoService.Notifications, n => n.Property == "Aluno" && n.Message == "O curso não pode ser nulo.");
            _cursoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Curso>()), Times.Never);
        }

        [Fact]
        public void Update_Should_AddNotification_When_CursoDoesNotExist()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            var curso = new Curso("Matemática Avançada", "Curso de matemática avançada", 60, 25, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddMonths(3));
            typeof(Curso).GetProperty(nameof(Curso.Id)).SetValue(curso, cursoId);

            _cursoRepositoryMock.Setup(repo => repo.GetById(cursoId)).Returns((Curso)null);

            // Act
            _cursoService.Update(curso);

            // Assert
            Assert.False(_cursoService.IsValid);
            Assert.Contains(_cursoService.Notifications, n => n.Property == nameof(curso.Id) && n.Message == "O curso não foi localizado.");
            _cursoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Curso>()), Times.Never);
        }

        [Fact]
        public void Update_Should_AddNotification_When_CursoIsInvalid()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            var curso = new Curso("", "", 0, 0, Guid.Empty, DateTime.Now, DateTime.Now.AddMonths(3)); // Dados inválidos
            typeof(Curso).GetProperty(nameof(Curso.Id)).SetValue(curso, cursoId);

            _cursoRepositoryMock.Setup(repo => repo.GetById(cursoId)).Returns(curso);

            // Act
            _cursoService.Update(curso);

            // Assert
            Assert.False(_cursoService.IsValid);
            Assert.Contains(_cursoService.Notifications, n => n.Property == "Titulo" && n.Message == "O título do curso é obrigatório.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "Titulo" && n.Message == "O título do curso deve ter no mínimo 5 caracteres.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "TempoDuracao" && n.Message == "O tempo de duração do curso deve ser maior que zero.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "NumeroVagas" && n.Message == "O número de vagas do curso deve ser maior que zero.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "DataInicio" && n.Message == "A data de início deve ser maior ou igual à data atual.");
            Assert.Contains(_cursoService.Notifications, n => n.Property == "ProfessorId" && n.Message == "O professor responsável pelo curso é obrigatório.");

            _cursoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Curso>()), Times.Never);
        }

        [Fact]
        public void Get_Should_ReturnCurso_When_CursoExists()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            var curso = new Curso("Matemática", "Curso de matemática básica", 40, 30, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddMonths(3));
            typeof(Curso).GetProperty(nameof(Curso.Id)).SetValue(curso, cursoId);

            _cursoRepositoryMock.Setup(repo => repo.GetById(cursoId)).Returns(curso);

            // Act
            var result = _cursoService.Get(cursoId);

            // Assert
            Assert.True(_cursoService.IsValid);
            Assert.Equal(curso, result);
        }

        [Fact]
        public void Get_Should_AddNotification_When_CursoDoesNotExist()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            _cursoRepositoryMock.Setup(repo => repo.GetById(cursoId)).Returns((Curso)null);

            // Act
            var result = _cursoService.Get(cursoId);

            // Assert
            Assert.False(_cursoService.IsValid);
            Assert.Contains(_cursoService.Notifications, n => n.Property == "curso" && n.Message == "curso não localizado");
            Assert.Null(result);
        }

        [Fact]
        public void Delete_Should_RemoveCurso_When_CursoExists()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            var curso = new Curso("Matemática", "Curso de matemática básica", 40, 30, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddMonths(3));
            typeof(Curso).GetProperty(nameof(Curso.Id)).SetValue(curso, cursoId);

            _cursoRepositoryMock.Setup(repo => repo.GetById(cursoId)).Returns(curso);

            // Act
            _cursoService.Delete(cursoId);

            // Assert
            Assert.True(_cursoService.IsValid);
            _cursoRepositoryMock.Verify(repo => repo.Delete(cursoId), Times.Once);
        }

        [Fact]
        public void Delete_Should_AddNotification_When_IdIsEmpty()
        {
            // Act
            _cursoService.Delete(Guid.Empty);

            // Assert
            Assert.False(_cursoService.IsValid);
            Assert.Contains(_cursoService.Notifications, n => n.Property == "id" && n.Message == "O Id do aluno é inválido.");
            _cursoRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void Delete_Should_AddNotification_When_CursoDoesNotExist()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            _cursoRepositoryMock.Setup(repo => repo.GetById(cursoId)).Returns((Curso)null);

            // Act
            _cursoService.Delete(cursoId);

            // Assert
            Assert.False(_cursoService.IsValid);
            Assert.Contains(_cursoService.Notifications, n => n.Property == "Curso" && n.Message == "O curso não foi encontrado.");
            _cursoRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
        }
    }
}
