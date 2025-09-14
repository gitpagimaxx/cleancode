namespace CleanCode.Domain.Interfaces;

public interface IBaseEntity<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T category);
    Task UpdateAsync(T category);
    Task DeleteAsync(T category);
}
