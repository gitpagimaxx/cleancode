using CleanCode.Application.Categories.Commands;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Categories.Handlers;

public class CategoryUpdateCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryUpdateCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException("Entity not found");

        category.Update(request.Name!);
        return await _categoryRepository.UpdateAsync(category, cancellationToken);
    }
}
