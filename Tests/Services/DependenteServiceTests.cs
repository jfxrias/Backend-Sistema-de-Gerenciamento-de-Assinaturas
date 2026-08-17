using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class DependenteServiceTests
    {
        private readonly Mock<IDependenteRepository> _dependenteRepositoryMock;
        private readonly DependenteService _dependenteService;

        public DependenteServiceTests()
        {
            _dependenteRepositoryMock = new Mock<IDependenteRepository>();
            _dependenteService = new DependenteService(_dependenteRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarAsync_DeveVincularDependenteAoAssinante()
        {
            var titularId = Guid.NewGuid();
            var dto = new DependenteCadastroDto { Nome = "Jorge", Email = "jorge@email.com", Senha = "123" };

            await _dependenteService.CadastrarAsync(titularId, dto);

            _dependenteRepositoryMock.Verify(repo => repo.CadastrarAsync(It.Is<Dependente>(d =>
                d.Nome == dto.Nome &&
                d.AssinanteId == titularId)), Times.Once);
        }

        [Fact]
        public async Task ObterPorAssinanteAsync_DeveRetornarListaDeDependentes()
        {
            var titularId = Guid.NewGuid();
            var listaMock = new List<Dependente> { new Dependente { Id = Guid.NewGuid(), Nome = "Dependente 1" } };

            _dependenteRepositoryMock.Setup(repo => repo.ObterTodosDoAssinanteAsync(titularId))
                .ReturnsAsync(listaMock);

            var resultado = await _dependenteService.ObterPorAssinanteAsync(titularId);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
        }
    }
}