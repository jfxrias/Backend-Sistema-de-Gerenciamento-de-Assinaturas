using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task CadastrarAsync(Usuario usuario);
        Task<Usuario?> ObterPorEmailAsync(string email);
    }
}