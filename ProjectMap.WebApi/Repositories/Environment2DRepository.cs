using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public class Environment2DRepository : IEnvironment2DRepository
    {
        private readonly string sqlConnectionString;

        public Environment2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Environment2D> InsertAsync(Environment2D environment2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO [Environment2D] (Id, Name, UserId) VALUES (@Id, @Name, @UserId)", environment2D);
                return environment2D;
            }
        }

        public async Task<Environment2D?> ReadByIdAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Environment2D>("SELECT * FROM [Environment2D] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Environment2D>> ReadByUserIdAsync(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Environment2D>(
                    "SELECT * FROM [Environment2D] WHERE UserId = @UserId", new { UserId = userId });
            }
        }

        public async Task<IEnumerable<Environment2D>> ReadAllAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Environment2D>("SELECT * FROM [Environment2D]");
            }
        }

        public async Task UpdateAsync(Environment2D environment2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Environment2D] SET " +
                                                 "Name = @Name, " +
                                                 "UserId = @UserId " +
                                                 "WHERE Id = @Id", environment2D);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Environment2D] WHERE Id = @Id", new { id });
            }
        }
    }
}
