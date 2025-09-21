using CleanCode.Domain.Entities;
using MediatR;

namespace CleanCode.Application.Products.Commands;

public class ProductRemoveCommand(int id) : IRequest<Product>
{
    public int Id { get; set; } = id;
}
