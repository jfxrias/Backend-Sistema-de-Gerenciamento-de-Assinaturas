using Domain.Entities;
using System;
using Xunit;

namespace Tests.Domain
{
    public class UsuarioTests
    {
        [Fact]
        public void CriarUsuario_ComDadosValidos_DeveAtribuirPropriedadesCorretamente()
        {
            var id = Guid.NewGuid();
            var nome = "João Gabriel";
            var email = "joao@teste.com";
            var senha = "senha_segura";
            var assinaturaId = Guid.NewGuid();

            var usuario = new Usuario
            {
                Id = id,
                Nome = nome,
                Email = email,
                Senha = senha,
                AssinaturaId = assinaturaId
            };

            // Assert
            Assert.Equal(id, usuario.Id);
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(senha, usuario.Senha);
            Assert.Equal(assinaturaId, usuario.AssinaturaId);
        }
    }
}