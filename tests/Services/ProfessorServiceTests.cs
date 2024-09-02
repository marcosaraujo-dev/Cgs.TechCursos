using System;
using Xunit;
using Moq;
using Cgs.TechCursos.Application.Services;
using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using System.Collections.Generic;


namespace Cgs.TechCursos.Tests.Services
{
    public class ProfessorServiceTests
    {
        private readonly Mock<IProfessorRepository> _professorRepositoryMock;
        private readonly ProfessorService _professorService;

        public ProfessorServiceTests()
        {
            _professorRepositoryMock = new Mock<IProfessorRepository>();
            _professorService = new ProfessorService(_professorRepositoryMock.Object);
        }

        [Fact]
        public void Create_Should_AddProfessor_When_ProfessorIsValid()
        {
            // Arrange
            var professor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com");

            // Act
            _professorService.Create(professor);

            // Assert
            Assert.True(_professorService.IsValid);
            _professorRepositoryMock.Verify(repo => repo.Add(professor), Times.Once);
        }

        [Fact]
        public void Create_Should_AddNotification_When_ProfessorIsNull()
        {
            // Act
            _professorService.Create(null);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "Professor" && n.Message == "O professor não pode ser nulo.");
            _professorRepositoryMock.Verify(repo => repo.Add(It.IsAny<Professor>()), Times.Never);
        }

        [Fact]
        public void Create_Should_AddNotification_When_EmailAlreadyExists()
        {
            // Arrange
            var existingProfessor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com");
            _professorRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Professor> { existingProfessor });

            var newProfessor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com");

            // Act
            _professorService.Create(newProfessor);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "Email" && n.Message == "Já existe um professor cadastrado com esse email.");
            _professorRepositoryMock.Verify(repo => repo.Add(It.IsAny<Professor>()), Times.Never);
        }

        [Fact]
        public void Create_Should_AddNotification_When_ProfessorIsInvalid()
        {
            // Arrange
            var professor = new Professor("", "email_invalido"); // Professor inválido (nome vazio, email inválido)

            // Act
            _professorService.Create(professor);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "Nome" && n.Message == "O nome não pode ser vazio");
            Assert.Contains(_professorService.Notifications, n => n.Property == "Nome" && n.Message == "O nome deve ter entre 3 e 100 caracteres");
            Assert.Contains(_professorService.Notifications, n => n.Property == "Email" && n.Message == "O email deve estar em um formato válido");
            _professorRepositoryMock.Verify(repo => repo.Add(It.IsAny<Professor>()), Times.Never);
        }

        [Fact]
        public void Update_Should_AddNotification_When_ProfessorIsNull()
        {
            // Act
            _professorService.Update(null);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "Professor" && n.Message == "O professor não pode ser nulo.");
            _professorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Professor>()), Times.Never);
        }

        [Fact]
        public void Update_Should_AddNotification_When_ProfessorNotFound()
        {
            // Arrange
            var professor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com");
            var id = Guid.NewGuid();
            typeof(Professor).GetProperty(nameof(Professor.Id)).SetValue(professor, id);

            _professorRepositoryMock.Setup(repo => repo.GetById(id)).Returns((Professor)null);

            // Act
            _professorService.Update(professor);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "Id" && n.Message == "O professor não foi localizado.");
            _professorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Professor>()), Times.Never);
        }

        [Fact]
        public void Update_Should_UpdateProfessor_When_ProfessorIsValid()
        {
            // Arrange
            var professor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com");
            var id = Guid.NewGuid();
            typeof(Professor).GetProperty(nameof(Professor.Id)).SetValue(professor, id);

            _professorRepositoryMock.Setup(repo => repo.GetById(id)).Returns(professor);

            // Act
            _professorService.Update(professor);

            // Assert
            Assert.True(_professorService.IsValid);
            _professorRepositoryMock.Verify(repo => repo.Update(professor), Times.Once);
        }

        [Fact]
        public void GetById_Should_AddNotification_When_IdIsEmpty()
        {
            // Act
            var result = _professorService.GetById(Guid.Empty);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "id" && n.Message == "O Id do professor é inválido.");
            Assert.Null(result);
        }

        [Fact]
        public void GetById_Should_AddNotification_When_ProfessorNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _professorRepositoryMock.Setup(repo => repo.GetById(id)).Returns((Professor)null);

            // Act
            var result = _professorService.GetById(id);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "Professor" && n.Message == "Professor não localizado.");
            Assert.Null(result);
        }

        [Fact]
        public void Delete_Should_AddNotification_When_IdIsEmpty()
        {
            // Act
            _professorService.Delete(Guid.Empty);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "id" && n.Message == "O Id do professor é inválido.");
            _professorRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void Delete_Should_AddNotification_When_ProfessorNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _professorRepositoryMock.Setup(repo => repo.GetById(id)).Returns((Professor)null);

            // Act
            _professorService.Delete(id);

            // Assert
            Assert.False(_professorService.IsValid);
            Assert.Contains(_professorService.Notifications, n => n.Property == "Professor" && n.Message == "O professor não foi encontrado.");
            _professorRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void Delete_Should_DeleteProfessor_When_ProfessorExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var professor = new Professor("Marcos Araujo", "marcos.araujo@cgs.com");
            typeof(Professor).GetProperty(nameof(Professor.Id)).SetValue(professor, id);

            _professorRepositoryMock.Setup(repo => repo.GetById(id)).Returns(professor);

            // Act
            _professorService.Delete(id);

            // Assert
            Assert.True(_professorService.IsValid);
            _professorRepositoryMock.Verify(repo => repo.Delete(id), Times.Once);
        }
    }

}
