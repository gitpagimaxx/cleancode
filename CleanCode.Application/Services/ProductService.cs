using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Application.Products.Commands;
using CleanCode.Application.Products.Queries;
using MediatR;

namespace CleanCode.Application.Services;

public class ProductService(
    IMapper mapper,
    IMediator mediator) : IProductService
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<ProductDto> AddAsync(ProductDto entity, CancellationToken token)
    {
        var entityCommand = _mapper.Map<ProductCreateCommand>(entity);

        if (entityCommand == null) throw new ArgumentNullException(nameof(entityCommand));

        var result = await _mediator.Send(entityCommand, token);

        return _mapper.Map<ProductDto>(result);
    }

    public async Task<ProductDto> DeleteAsync(int id, CancellationToken token)
    {
        var entityCommand = new ProductRemoveCommand(id);

        if (entityCommand == null) throw new ArgumentNullException(nameof(entityCommand));

        var result = await _mediator.Send(entityCommand, token);

        return _mapper.Map<ProductDto>(result);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var productsQuery = new GetProductsQuery();

        if (productsQuery == null) throw new ArgumentNullException(nameof(productsQuery));

        var result = await _mediator.Send(productsQuery);

        return _mapper.Map<IEnumerable<ProductDto>>(result);
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var productsQuery = new GetProductByIdQuery(id);

        if (productsQuery == null) throw new ArgumentNullException(nameof(productsQuery));

        var result = await _mediator.Send(productsQuery);

        return _mapper.Map<ProductDto?>(result);
    }

    public async Task<ProductDto> UpdateAsync(ProductDto entity, CancellationToken token)
    {
        var entityCommand = _mapper.Map<ProductUpdateCommand>(entity);

        if (entityCommand == null) throw new ArgumentNullException(nameof(entityCommand));

        var result = await _mediator.Send(entityCommand, token);

        return _mapper.Map<ProductDto>(result);
    }
}
