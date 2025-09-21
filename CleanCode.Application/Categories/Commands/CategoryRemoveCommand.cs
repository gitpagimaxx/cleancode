using CleanCode.Domain.Entities;
using MediatR;

namespace CleanCode.Application.Categories.Commands;

public class CategoryRemoveCommand(int id) : IRequest<Category>
{
    public int Id { get; set; } = id;
}
