using CleanCode.Application.Products.Queries;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Products.Handlers;

public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync();
    }
}
