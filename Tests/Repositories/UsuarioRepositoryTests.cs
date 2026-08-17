using Domain.Entities;
using Domain.Repositories;
using Moq;
using Moq.Dapper;
using System;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Repositories
{
    public class UsuarioRepositoryTests
    {
        // mock de conexão com o banco
        private readonly Mock<IDbConnection> _dbConnectionMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        public UsuarioRepositoryTests()
        {
            _dbConnectionMock = new Mock<IDbConnection>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async Task ObterPorEmailESenhaAsync_DeveRetornarUsuario_QuandoCredenciaisCorretas()
        {
            var email = "joao@teste.com";
            var senha = "123";
            var usuarioEsperado = new Usuario { Id = Guid.NewGuid(), Email = email, Nome = "João" };

            _usuarioRepositoryMock.Setup(repo => repo.ObterPorEmailESenhaAsync(email, senha))
                .ReturnsAsync(usuarioEsperado);

            var resultado = await _usuarioRepositoryMock.Object.ObterPorEmailESenhaAsync(email, senha);

            Assert.NotNull(resultado);
            Assert.Equal(email, resultado.Email);
            Assert.Equal("João", resultado.Nome);
        }

        [Fact]
        public async Task CadastrarAsync_DeveExecutarSemErros()
        {
            var novoUsuario = new Usuario { Id = Guid.NewGuid(), Nome = "Teste", Email = "teste@teste.com" };

            _usuarioRepositoryMock.Setup(repo => repo.CadastrarAsync(novoUsuario))
                .Returns(Task.CompletedTask);

            var exception = await Record.ExceptionAsync(() => _usuarioRepositoryMock.Object.CadastrarAsync(novoUsuario));

            Assert.Null(exception);
        }
    }
}