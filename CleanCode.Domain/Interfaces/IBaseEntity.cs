namespace CleanCode.Domain.Interfaces;

public interface IBaseEntity<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T category);
    Task<T> UpdateAsync(T category);
    Task<T> DeleteAsync(T category);
}
