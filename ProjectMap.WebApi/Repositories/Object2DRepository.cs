using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public class Object2DRepository : IObject2DRepository
    {
        private readonly string sqlConnectionString;

        public Object2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Object2D> InsertAsync(Object2D object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO [Object2D] (Id, PrefabId, PositionX, PositionY, Environment2DId) VALUES (@Id, @PrefabId, @PositionX, @PositionY, @Environment2DId)", object2D);
                return object2D;
            }
        }

        public async Task<Object2D?> ReadByIdAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Object2D>("SELECT * FROM [Object2D] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Object2D>> ReadByEnvironmentIdAsync(Guid environment2DId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D] WHERE Environment2DId = @Environment2DId", new { environment2DId });
            }
        }

        public async Task<IEnumerable<Object2D>> ReadByUserIdAsync(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>(
                    "SELECT o.* FROM [Object2D] o JOIN [Environment2D] e ON o.Environment2DId = e.Id WHERE e.UserId = @UserId", new { UserId = userId });
            }
        }

        public async Task UpdateAsync(Object2D object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Object2D] SET " +
                                                 "PrefabId = @PrefabId, " +
                                                 "PositionX = @PositionX, " +
                                                 "PositionY = @PositionY, " +
                                                 "Environment2DId = @Environment2DId " +
                                                 "WHERE Id = @Id", object2D);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Object2D] WHERE Id = @Id", new { id });
            }
        }
    }
}


