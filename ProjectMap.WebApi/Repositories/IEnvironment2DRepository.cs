using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public interface IEnvironment2DRepository
    {
        Task DeleteAsync(Guid id);
        Task<Environment2D> InsertAsync(Environment2D environment2D);
        Task<IEnumerable<Environment2D>> ReadAllAsync();
        Task<Environment2D?> ReadByIdAsync(Guid id);
        Task<IEnumerable<Environment2D>> ReadByUserIdAsync(Guid userId); // New method
        Task UpdateAsync(Environment2D environment2D);
    }
}