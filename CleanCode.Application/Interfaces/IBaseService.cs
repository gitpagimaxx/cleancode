namespace CleanCode.Application.Interfaces;

public interface IBaseService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T category, CancellationToken token);
    Task<T> UpdateAsync(T category, CancellationToken token);
    Task<T> DeleteAsync(int id, CancellationToken token);
}