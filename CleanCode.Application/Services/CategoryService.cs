using AutoMapper;
using CleanCode.Application.Categories.Commands;
using CleanCode.Application.Categories.Queries;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Application.Products.Commands;
using CleanCode.Application.Products.Queries;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using MediatR;

namespace CleanCode.Application.Services;

public class CategoryService(
    IMapper mapper,
    IMediator mediator) : ICategoryService
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<CategoryDto> AddAsync(CategoryDto entity, CancellationToken token)
    {
        var entityCommand = _mapper.Map<CategoryCreateCommand>(entity);

        if (entityCommand == null) throw new ArgumentNullException(nameof(entityCommand));

        var result = await _mediator.Send(entityCommand, token);

        return _mapper.Map<CategoryDto>(result);
    }

    public async Task<CategoryDto> DeleteAsync(int id, CancellationToken token)
    {
        var entityCommand = new CategoryRemoveCommand(id);

        if (entityCommand == null) throw new ArgumentNullException(nameof(entityCommand));

        var result = await _mediator.Send(entityCommand, token);

        return _mapper.Map<CategoryDto>(result);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var entityQuery = new GetCategoriesQuery();

        if (entityQuery == null) throw new ArgumentNullException(nameof(entityQuery));

        var result = await _mediator.Send(entityQuery);

        return _mapper.Map<IEnumerable<CategoryDto>>(result);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var commandQuery = new GetCategoryByIdQuery(id);

        if (commandQuery == null) throw new ArgumentNullException(nameof(commandQuery));

        var result = await _mediator.Send(commandQuery);

        return _mapper.Map<CategoryDto?>(result);
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto entity, CancellationToken token)
    {
        var entityCommand = _mapper.Map<CategoryUpdateCommand>(entity);

        if (entityCommand == null) throw new ArgumentNullException(nameof(entityCommand));

        var result = await _mediator.Send(entityCommand, token);

        return _mapper.Map<CategoryDto>(result);
    }
}
