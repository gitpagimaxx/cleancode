using CleanCode.Domain.Entities;
using MediatR;

namespace CleanCode.Application.Categories.Queries;

public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
{
}
