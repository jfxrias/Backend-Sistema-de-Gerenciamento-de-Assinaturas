using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDependenteRepository _dependenteRepository;
        private readonly TokenService _tokenService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IDependenteRepository dependenteRepository,
            TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _dependenteRepository = dependenteRepository;
            _tokenService = tokenService;
        }

        public async Task CadastrarAsync(UsuarioCadastroDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            await _usuarioRepository.CadastrarAsync(usuario);
        }

        public async Task AtualizarPerfilAsync(Guid id, UsuarioEdicaoDto dto)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;

            await _usuarioRepository.AtualizarAsync(usuario);
        }

        public async Task<string> LoginAsync(UsuarioLoginDto dto)
        {
            var usuario = await _usuarioRepository.ObterPorEmailESenhaAsync(dto.Email, dto.Senha);

            if (usuario != null)
            {
                return _tokenService.GerarToken(usuario);
            }

            var dependente = await _dependenteRepository.ObterPorEmailESenhaAsync(dto.Email, dto.Senha);

            if (dependente != null)
            {
                return _tokenService.GerarTokenDependente(dependente);
            }

            throw new Exception("E-mail ou senha inválidos.");
        }
    }
}