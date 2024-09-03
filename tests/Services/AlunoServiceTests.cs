using Cgs.TechCursos.Application.Services;
using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cgs.TechCursos.Tests.Services
{
    public class AlunoServiceTests
    {
        private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
        private readonly AlunoService _alunoService;

        public AlunoServiceTests()
        {
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _alunoService = new AlunoService(_alunoRepositoryMock.Object);
        }

        [Fact]
        public void Create_Should_AddAluno_When_AlunoIsValid()
        {
            // Arrange
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", new DateTime(1995, 10, 20));

            // Act
            _alunoService.Create(aluno);

            // Assert
            Assert.True(_alunoService.IsValid);
            _alunoRepositoryMock.Verify(repo => repo.Add(aluno), Times.Once);
        }

        [Fact]
        public void Create_Should_GeneratedMatricula_When_AlunoIsValid()
        {
            // Arrange
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12345678901", new DateTime(1995, 10, 20));

            // Act
            var matricula = aluno.Matricula;

            // Assert
            matricula.Should().NotBeNullOrEmpty();
            matricula.Length.Should().Be(9); // 9 caracteres
        }

        [Fact]
        public void Create_Should_AddNotification_When_AlunoIsInvalid()
        {
            // Arrange
            var aluno = new Aluno("", "invalid-email", "1231231239", new DateTime(2025, 08, 12));

            // Act
            _alunoService.Create(aluno);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Nome" && n.Message == "O nome não pode ser vazio.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Nome" && n.Message == "O nome deve ter entre 3 e 100 caracteres.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Email" && n.Message == "O e-mail deve ser válido.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Documento" && n.Message == "O documento deve ter 11 ou 14 caracteres.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "DataNascimento" && n.Message == "A data de nascimento deve ser no passado.");
            _alunoRepositoryMock.Verify(repo => repo.Add(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void Create_Should_AddNotification_When_DataNascimento_IsInFuture()
        {
            // Arrange
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", DateTime.Now.AddYears(1));

            // Act
            _alunoService.Create(aluno);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == "DataNascimento" && n.Message == "A data de nascimento deve ser no passado.");
            _alunoRepositoryMock.Verify(repo => repo.Add(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void Update_Should_UpdateAluno_When_AlunoIsValid()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", new DateTime(1995, 10, 20));
            typeof(Aluno).GetProperty(nameof(Aluno.Id)).SetValue(aluno, alunoId);

            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns(aluno);

            // Act
            _alunoService.Update(aluno);

            // Assert
            Assert.True(_alunoService.IsValid);
            _alunoRepositoryMock.Verify(repo => repo.Update(aluno), Times.Once);
        }

        [Fact]
        public void Update_Should_AddNotification_When_AlunoIsNull()
        {
            // Act
            _alunoService.Update(null);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Aluno" && n.Message == "O aluno não pode ser nulo.");
            _alunoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void Update_Should_AddNotification_When_AlunoDoesNotExist()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", new DateTime(1995, 10, 20));
            typeof(Aluno).GetProperty(nameof(Aluno.Id)).SetValue(aluno, alunoId);

            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns((Aluno)null);

            // Act
            _alunoService.Update(aluno);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == nameof(aluno.Id) && n.Message == "O aluno não foi localizado.");
            _alunoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void Update_Should_AddNotification_When_AlunoIsInvalid()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var aluno = new Aluno("", "invalid-email", "12312456756312390", new DateTime(2025, 10, 20));
            typeof(Aluno).GetProperty(nameof(Aluno.Id)).SetValue(aluno, alunoId);

            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns(aluno);

            // Act
            _alunoService.Update(aluno);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Nome" && n.Message == "O nome não pode ser vazio.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Nome" && n.Message == "O nome deve ter entre 3 e 100 caracteres.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Email" && n.Message == "O e-mail deve ser válido.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Documento" && n.Message == "O documento deve ter 11 ou 14 caracteres.");
            Assert.Contains(_alunoService.Notifications, n => n.Property == "DataNascimento" && n.Message == "A data de nascimento deve ser no passado.");
            _alunoRepositoryMock.Verify(repo => repo.Update(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void Get_Should_ReturnAluno_When_AlunoExists()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", new DateTime(1995, 10, 20));
            typeof(Aluno).GetProperty(nameof(Aluno.Id)).SetValue(aluno, alunoId);

            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns(aluno);

            // Act
            var result = _alunoService.Get(alunoId);

            // Assert
            Assert.True(_alunoService.IsValid);
            Assert.Equal(aluno, result);
        }

        [Fact]
        public void Get_Should_AddNotification_When_AlunoDoesNotExist()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns((Aluno)null);

            // Act
            var result = _alunoService.Get(alunoId);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Aluno" && n.Message == "Aluno não localizado");
            Assert.Null(result);
        }

        [Fact]
        public void Get_Should_AddNotification_When_IdIsValidButAlunoDoesNotExist()
        {
            // Arrange
            var alunoId = Guid.NewGuid();

            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns((Aluno)null);

            // Act
            var result = _alunoService.Get(alunoId);

            // Assert
            result.Should().BeNull();
            _alunoService.Notifications.Should().ContainSingle(n => n.Property == "Aluno" && n.Message == "Aluno não localizado");
        }

        [Fact]
        public void Get_Should_AddNotification_When_IdIsInvalid()
        {
            // Act
            var result = _alunoService.Get(Guid.Empty);

            // Assert
            result.Should().BeNull();
            _alunoService.Notifications.Should().ContainSingle(n => n.Property == "id" && n.Message == "O Id do aluno é inválido.");
        }

        [Fact]
        public void Delete_Should_RemoveAluno_When_AlunoExists()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var aluno = new Aluno("Marcos Araujo", "marcos.araujo@cgs.com", "12312312390", new DateTime(1995, 10, 20) );
            typeof(Aluno).GetProperty(nameof(Aluno.Id)).SetValue(aluno, alunoId);

            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns(aluno);

            // Act
            _alunoService.Delete(alunoId);

            // Assert
            Assert.True(_alunoService.IsValid);
            _alunoRepositoryMock.Verify(repo => repo.Delete(alunoId), Times.Once);
        }

        [Fact]
        public void Delete_Should_AddNotification_When_IdIsEmpty()
        {
            // Act
            _alunoService.Delete(Guid.Empty);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == "id" && n.Message == "O Id do aluno é inválido.");
            _alunoRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void Delete_Should_AddNotification_When_AlunoDoesNotExist()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            _alunoRepositoryMock.Setup(repo => repo.GetById(alunoId)).Returns((Aluno)null);

            // Act
            _alunoService.Delete(alunoId);

            // Assert
            Assert.False(_alunoService.IsValid);
            Assert.Contains(_alunoService.Notifications, n => n.Property == "Aluno" && n.Message == "O aluno não foi encontrado.");
            _alunoRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Never);
        }
    }
}
