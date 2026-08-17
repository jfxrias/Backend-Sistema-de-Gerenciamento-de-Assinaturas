using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task CadastrarAsync(Usuario usuario)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "INSERT INTO usuarios (nome, email, senha, assinaturaid) VALUES ( @Nome, @Email, @Senha, @AssinaturaId)";

            await dbConnection.ExecuteAsync(sql, usuario);
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "SELECT id, nome, email, senha, assinaturaid FROM usuarios WHERE email = @Email";

            return await dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email });
        }

        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "SELECT id, nome, email, senha, assinaturaid FROM usuarios WHERE id = @Id";

            return await dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "UPDATE usuarios SET nome = @Nome, email = @Email, senha = @Senha, assinaturaid = @AssinaturaId WHERE id = @Id";

            await dbConnection.ExecuteAsync(sql, usuario);
        }

        public async Task<Usuario?> ObterPorEmailESenhaAsync(string email, string senha)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
            string sql = "SELECT id, nome, email, senha, assinaturaid FROM usuarios WHERE email = @Email AND senha = @Senha";

            return await dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email, Senha = senha });
        }
    }
}