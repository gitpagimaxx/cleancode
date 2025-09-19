using CleanCode.Domain.Entities;

namespace CleanCode.Domain.Interfaces;

public interface IProductRepository : IBaseEntity<Product>
{
    Task<Product?> GetProductsByCategoryAsync(int categoryId);
}
