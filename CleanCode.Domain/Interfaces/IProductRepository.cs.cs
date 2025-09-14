using CleanCode.Domain.Entities;

namespace CleanCode.Domain.Interfaces;

public interface IProductRepository : IBaseEntity<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
}
