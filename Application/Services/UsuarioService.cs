using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly TokenService _tokenService;

        public UsuarioService(IUsuarioRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task CadastrarAsync(UsuarioCadastroDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha // se der tempo vou colocar bcrypt
            };

            await _repository.CadastrarAsync(usuario);
        }

        public async Task<string> LoginAsync(UsuarioLoginDto dto)
        {
            var usuario = await _repository.ObterPorEmailAsync(dto.Email);

            if (usuario == null || usuario.Senha != dto.Senha)
            {
                throw new Exception("Email ou senha inválidos.");
            }
            return _tokenService.GerarToken(usuario);
        }
    }
}