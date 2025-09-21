using CleanCode.Application.Categories.Queries;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Categories.Handlers;

internal class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAllAsync();
    }
}
