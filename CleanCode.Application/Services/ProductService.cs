using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Domain.Interfaces;

namespace CleanCode.Application.Services;

public class ProductService(
    IProductRepository productRepository,
    IMapper mapper) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly IMapper _mapper = mapper;

    public Task<ProductDto> AddAsync(ProductDto entity)
    {
        var product = _mapper.Map<Domain.Entities.Product>(entity);
        var result = _productRepository.AddAsync(product);
        return _mapper.Map<Task<ProductDto>>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDto?>(product);
    }

    public async Task<ProductDto?> GetProductsByCategoryAsync(int entityId)
    {
        var product = await _productRepository.GetProductsByCategoryAsync(entityId);
        return _mapper.Map<ProductDto?>(product);
    }

    public async Task<ProductDto> UpdateAsync(ProductDto entity)
    {
        var product = _mapper.Map<Domain.Entities.Product>(entity);
        var result = await _productRepository.UpdateAsync(product);
        return _mapper.Map<ProductDto>(result);
    }
}
