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

        //injecao de dependencia, parece bastante com o que eu fazia no spring
        public UsuarioRepository(IConfiguration configuration)
        {
            // tenho que setar defaultconnection no appsettings
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task CadastrarAsync(Usuario usuario)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);

            //funciona diferente do springboot, aq nn tnho q colocar que id e data são criados pelo bd
            string sql = @"
                INSERT INTO Usuarios (Nome, Email, Senha) 
                VALUES (@Nome, @Email, @Senha)";

            await dbConnection.ExecuteAsync(sql, usuario);
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);

            string sql = "SELECT * FROM Usuarios WHERE Email = @Email";

            //retorna usuario ou null
            return await dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email });
        }
    }
}