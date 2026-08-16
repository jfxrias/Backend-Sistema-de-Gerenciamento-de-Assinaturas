using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IDependenteRepository
    {
        Task CadastrarAsync(Dependente dependente);
        Task<IEnumerable<Dependente>> ObterTodosDoAssinanteAsync(Guid assinanteId);
        Task AtualizarAsync(Dependente dependente);
        Task DeletarAsync(Guid id, Guid assinanteId);
        Task<Dependente> ObterPorEmailESenhaAsync(string email, string senha);
    }
}