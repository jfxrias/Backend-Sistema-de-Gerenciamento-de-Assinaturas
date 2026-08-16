using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DependenteService
    {
        private readonly IDependenteRepository _repository;

        public DependenteService(IDependenteRepository repository)
        {
            _repository = repository;
        }
        public async Task CadastrarAsync(Guid usuarioLogadoId, DependenteCadastroDto dto)
        {
            var dependente = new Dependente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                AssinanteId = usuarioLogadoId
            };

            await _repository.CadastrarAsync(dependente);
        }

        public async Task<IEnumerable<Dependente>> ObterPorAssinanteAsync(Guid assinanteId)
        {
            return await _repository.ObterTodosDoAssinanteAsync(assinanteId);
        }

        public async Task AtualizarAsync(Guid id, Guid usuarioLogadoId, DependenteCadastroDto dto)
        {
            var dependente = new Dependente
            {
                Id = id,
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                AssinanteId = usuarioLogadoId
            };

            await _repository.AtualizarAsync(dependente);
        }

        public async Task DeletarAsync(Guid id, Guid usuarioLogadoId)
        {
            await _repository.DeletarAsync(id, usuarioLogadoId);
        }
    }
}