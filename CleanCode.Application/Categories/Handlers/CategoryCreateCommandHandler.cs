using CleanCode.Application.Categories.Commands;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Categories.Handlers;

public class CategoryCreateCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryCreateCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Category> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name!) ?? throw new ApplicationException("Error creating entity");

        return await _categoryRepository.AddAsync(category, cancellationToken);
    }
}
