using CleanCode.Application.Products.Commands;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Products.Handlers;

public class ProductRemoveCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductRemoveCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.DeleteAsync(request.Id, cancellationToken);

        return result;
    }
}
