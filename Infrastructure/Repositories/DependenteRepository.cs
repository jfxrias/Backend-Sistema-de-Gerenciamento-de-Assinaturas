using System.Data;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class DependenteRepository : IDependenteRepository
    {
        private readonly string _connectionString;

        public DependenteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task CadastrarAsync(Dependente dependente)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);

            string sql = @"
                INSERT INTO dependentes (nome, email, senha, assinanteid) 
                VALUES (@Nome, @Email, @Senha, @AssinanteId)";

            await dbConnection.ExecuteAsync(sql, dependente);
        }

        public async Task<IEnumerable<Dependente>> ObterTodosDoAssinanteAsync(Guid assinanteId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "SELECT id, nome, email, senha, assinanteid FROM dependentes WHERE assinanteid = @AssinanteId";

            return await dbConnection.QueryAsync<Dependente>(sql, new { AssinanteId = assinanteId });
        }

        public async Task AtualizarAsync(Dependente dependente)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = @"
                UPDATE dependentes 
                SET nome = @Nome, email = @Email, senha = @Senha 
                WHERE id = @Id AND assinanteid = @AssinanteId";

            await dbConnection.ExecuteAsync(sql, dependente);
        }

        public async Task DeletarAsync(Guid id, Guid assinanteId)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "DELETE FROM dependentes WHERE id = @Id AND assinanteid = @AssinanteId";

            await dbConnection.ExecuteAsync(sql, new { Id = id, AssinanteId = assinanteId });
        }

        public async Task<Dependente> ObterPorEmailESenhaAsync(string email, string senha)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "SELECT id, nome, email, senha, assinanteid FROM dependentes WHERE email = @Email AND senha = @Senha";
            return await dbConnection.QueryFirstOrDefaultAsync<Dependente>(sql, new { Email = email, Senha = senha });
        }
    }
}