namespace CleanCode.Application.Interfaces;

public interface IBaseService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T category);
    Task<T> UpdateAsync(T category);
    Task DeleteAsync(int id);
}