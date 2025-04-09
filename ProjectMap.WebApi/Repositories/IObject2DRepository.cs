using ProjectMap.WebApi.Models;

public interface IObject2DRepository
{
    Task DeleteAsync(Guid id);
    Task<Object2D> InsertAsync(Object2D object2D);
    Task<IEnumerable<Object2D>> ReadByEnvironmentIdAsync(Guid environment2DId);
    Task<Object2D?> ReadByIdAsync(Guid id);
    Task<IEnumerable<Object2D>> ReadByUserIdAsync(Guid userId); // Toegevoegd
    Task UpdateAsync(Object2D object2D);
}
