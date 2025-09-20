using CleanCode.Domain.Entities;
using MediatR;

namespace CleanCode.Application.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}
