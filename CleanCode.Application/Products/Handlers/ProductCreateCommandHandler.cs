using CleanCode.Application.Products.Commands;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Products.Handlers;

public class ProductCreateCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name!, request.Description!, request.Price, request.Stock, request.Image!) ?? throw new ApplicationException("Error creating entity");

        product.CategoryId = request.CategoryId;

        return await _productRepository.AddAsync(product, cancellationToken);
    }
}
