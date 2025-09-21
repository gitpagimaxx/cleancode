using CleanCode.Application.Categories.Commands;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Categories.Handlers;

public class CategoryRemoveCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryRemoveCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Category> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
