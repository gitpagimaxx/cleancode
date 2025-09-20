using CleanCode.Application.Dtos;

namespace CleanCode.Application.Interfaces;

public interface IProductService : IBaseService<ProductDto>
{
    Task<ProductDto?> GetProductsByCategoryAsync(int categoryId);
}
