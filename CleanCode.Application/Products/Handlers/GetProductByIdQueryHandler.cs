using CleanCode.Application.Products.Queries;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Products.Handlers;

public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException("Product not found");
    }
}
