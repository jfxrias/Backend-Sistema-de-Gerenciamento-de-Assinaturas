using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IDependenteRepository> _dependenteRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TokenService _tokenService;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _dependenteRepositoryMock = new Mock<IDependenteRepository>();

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(x => x["Jwt:Key"]).Returns("ChaveSuperSecretaParaTesteDeJwt1234567890");

            _tokenService = new TokenService(_configurationMock.Object);

            _usuarioService = new UsuarioService(
                _usuarioRepositoryMock.Object,
                _dependenteRepositoryMock.Object,
                _tokenService);
        }

        [Fact]
        public async Task LoginAsync_CredenciaisInvalidas_DeveLancarExcecao()
        {
            var dto = new UsuarioLoginDto { Email = "errado@teste.com", Senha = "123" };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorEmailESenhaAsync(dto.Email, dto.Senha))
                .ReturnsAsync((Usuario?)null);

            _dependenteRepositoryMock.Setup(repo => repo.ObterPorEmailESenhaAsync(dto.Email, dto.Senha))
                .ReturnsAsync((Dependente?)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _usuarioService.LoginAsync(dto));
            Assert.Equal("E-mail ou senha inválidos.", exception.Message);
        }

        [Fact]
        public async Task CadastrarAsync_DeveChamarRepositorioParaSalvar()
        {
            var dto = new UsuarioCadastroDto
            {
                Nome = "João",
                Email = "joao@teste.com",
                Senha = "123",
                AssinaturaId = Guid.NewGuid()
            };

            await _usuarioService.CadastrarAsync(dto);

            _usuarioRepositoryMock.Verify(repo => repo.CadastrarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_CredenciaisValidasDeTitular_DeveRetornarTokenERole()
        {
            var dto = new UsuarioLoginDto { Email = "titular@teste.com", Senha = "123" };
            var planoId = Guid.NewGuid();

            var titularMock = new Usuario { Id = Guid.NewGuid(), Email = dto.Email, Nome = "Titular", AssinaturaId = planoId };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorEmailESenhaAsync(dto.Email, dto.Senha))
                .ReturnsAsync(titularMock);

            var resultado = await _usuarioService.LoginAsync(dto);

            Assert.NotNull(resultado);

            var roleProperty = resultado.GetType().GetProperty("role")?.GetValue(resultado, null);
            var assinaturaIdProperty = resultado.GetType().GetProperty("assinaturaId")?.GetValue(resultado, null);

            Assert.Equal("Titular", roleProperty);
            Assert.Equal(planoId, assinaturaIdProperty);
        }
    }
}