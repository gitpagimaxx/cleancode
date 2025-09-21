using CleanCode.Domain.Entities;
using MediatR;

namespace CleanCode.Application.Categories.Commands;

public abstract class CategoryCommand : IRequest<Category>
{
    public string Name { get; set; } = string.Empty;
}
