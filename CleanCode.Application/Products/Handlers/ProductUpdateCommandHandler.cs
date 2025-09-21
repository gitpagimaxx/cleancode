using CleanCode.Application.Products.Commands;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Products.Handlers;

public class ProductUpdateCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException($"Error could not be found");

        product.Update(request.Name!, request.Description!, request.Price, request.Stock, request.Image!, request.CategoryId);

        return await _productRepository.UpdateAsync(product, cancellationToken);
    }
}
