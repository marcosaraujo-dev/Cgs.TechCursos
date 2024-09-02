using Cgs.TechCursos.Application.Services;
using Cgs.TechCursos.Domain.Contracts;
using Cgs.TechCursos.Domain.Entities;
using Cgs.TechCursos.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cgs.TechCursos.Tests.Services
{
    public class InscricaoServiceTests
    {
        private readonly Mock<IInscricaoRepository> _inscricaoRepositoryMock;
        private readonly InscricaoService _inscricaoService;

        public InscricaoServiceTests()
        {
            _inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            _inscricaoService = new InscricaoService(_inscricaoRepositoryMock.Object);
        }

        [Fact]
        public void Create_Should_AddInscricao_When_InscricaoIsValid()
        {
            // Arrange
            var inscricao = new Inscricao(Guid.NewGuid(), Guid.NewGuid(), InscricaoStatus.Pendente);

            // Act
            var result = _inscricaoService.Create(inscricao);

            // Assert
            Assert.True(result);
            Assert.True(_inscricaoService.IsValid);
            _inscricaoRepositoryMock.Verify(repo => repo.Add(inscricao), Times.Once);
        }

        [Fact]
        public void Create_Should_AddNotification_When_InscricaoIsNull()
        {
            // Act
            var result = _inscricaoService.Create(null);

            // Assert
            Assert.False(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Property == "Inscrição" && n.Message == "Inscrição não pode ser nula.");
        }

        [Fact]
        public void Create_Should_AddNotification_When_AlunoAlreadyInscritoInCurso()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();
            var existingInscricao = new Inscricao(alunoId, cursoId, InscricaoStatus.Completa);
            _inscricaoRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Inscricao> { existingInscricao });

            var newInscricao = new Inscricao(alunoId, cursoId, InscricaoStatus.Completa);

            // Act
            var result = _inscricaoService.Create(newInscricao);

            // Assert
            Assert.False(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Property == "Inscrição" && n.Message == "O aluno já está inscrito neste curso.");
        }

        [Fact]
        public void Update_ShouldAddNotification_WhenInscricaoIsNull()
        {
            // Act
            var result = _inscricaoService.Update(null);

            // Assert
            Assert.False(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Message == "Inscrição não pode ser nula.");
        }

        [Fact]
        public void Update_Should_AddNotification_When_InscricaoNotFound()
        {
            // Arrange
            var inscricao = new Inscricao(Guid.NewGuid(), Guid.NewGuid(), InscricaoStatus.Completa);
            _inscricaoRepositoryMock.Setup(repo => repo.GetById(inscricao.Id)).Returns((Inscricao)null);

            // Act
            var result = _inscricaoService.Update(inscricao);

            // Assert
            Assert.False(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Property == "Inscrição" && n.Message == "Inscrição não encontrada.");
        }

        [Fact]
        public void Update_ShouldUpdateInscricao_WhenInscricaoIsValid()
        {
            // Arrange
            var inscricao = new Inscricao(Guid.NewGuid(), Guid.NewGuid(), InscricaoStatus.Completa);
            _inscricaoRepositoryMock.Setup(repo => repo.GetById(inscricao.Id)).Returns(inscricao);

            // Act
            var result = _inscricaoService.Update(inscricao);

            // Assert
            Assert.True(result);
            Assert.True(_inscricaoService.IsValid);
            _inscricaoRepositoryMock.Verify(repo => repo.Update(inscricao), Times.Once);
        }

        [Fact]
        public void GetById_ShouldAddNotification_WhenIdIsEmpty()
        {
            // Act
            var result = _inscricaoService.GetById(Guid.Empty);

            // Assert
            Assert.Null(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Message == "Id da inscrição inválido.");
        }

        [Fact]
        public void GetById_ShouldAddNotification_WhenInscricaoNotFound()
        {
            // Arrange
            var inscricaoId = Guid.NewGuid();
            _inscricaoRepositoryMock.Setup(repo => repo.GetById(inscricaoId)).Returns((Inscricao)null);

            // Act
            var result = _inscricaoService.GetById(inscricaoId);

            // Assert
            Assert.Null(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Message == "Inscrição não encontrada.");
        }

        [Fact]
        public void GetById_ShouldReturnInscricao_WhenInscricaoIsFound()
        {
            // Arrange
            var inscricao = new Inscricao(Guid.NewGuid(), Guid.NewGuid(), InscricaoStatus.Completa);
            _inscricaoRepositoryMock.Setup(repo => repo.GetById(inscricao.Id)).Returns(inscricao);

            // Act
            var result = _inscricaoService.GetById(inscricao.Id);

            // Assert
            Assert.NotNull(result);
            Assert.True(_inscricaoService.IsValid);
            Assert.Equal(inscricao, result);
        }

        [Fact]
        public void Delete_Should_AddNotification_When_InscricaoNotFound()
        {
            // Arrange
            var inscricaoId = Guid.NewGuid();
            _inscricaoRepositoryMock.Setup(repo => repo.GetById(inscricaoId)).Returns((Inscricao)null);

            // Act
            var result = _inscricaoService.Delete(inscricaoId);

            // Assert
            Assert.False(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Property == "Inscrição" && n.Message == "Inscrição não encontrada.");
        }

        [Fact]
        public void Delete_ShouldAddNotification_WhenIdIsEmpty()
        {
            // Act
            var result = _inscricaoService.Delete(Guid.Empty);

            // Assert
            Assert.False(result);
            Assert.False(_inscricaoService.IsValid);
            Assert.Contains(_inscricaoService.Notifications, n => n.Message == "Id da inscrição inválido.");
        }

        [Fact]
        public void Delete_ShouldDeleteInscricao_WhenInscricaoIsFound()
        {
            // Arrange
            var inscricao = new Inscricao(Guid.NewGuid(), Guid.NewGuid(), InscricaoStatus.Pendente);
            _inscricaoRepositoryMock.Setup(repo => repo.GetById(inscricao.Id)).Returns(inscricao);

            // Act
            var result = _inscricaoService.Delete(inscricao.Id);

            // Assert
            Assert.True(result);
            Assert.True(_inscricaoService.IsValid);
            _inscricaoRepositoryMock.Verify(repo => repo.Delete(inscricao.Id), Times.Once);
        }

    }
}
