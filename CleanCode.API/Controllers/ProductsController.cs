using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CleanCode.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(
    ILogger<ProductsController> logger, 
    IProductService productService) : ControllerBase
{
    private readonly ILogger<ProductsController> _logger = logger;
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogInformation("Accessed Products Index");

        var entity = await _productService.GetAllAsync();

        if (entity == null)
            return NotFound("Nenhum produto encontrado");

        return Ok(entity);
    }

    [HttpGet("{id:int}", Name = "GetProductByIdAsync")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var entity = await _productService.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] ProductDto entity, CancellationToken token)
    {
        if (entity == null)
            return BadRequest("Entity is null");

        var result = await _productService.AddAsync(entity, token);
        return new CreatedAtRouteResult("GetProductByIdAsync", new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductDto entity, CancellationToken cancellationToken)
    {
        if (id != entity.Id)
            return BadRequest("ID mismatch");

        if (entity == null)
            return BadRequest("Os dados não foram enviados");

        if (ModelState.IsValid)
        {
            var existingEntity = await _productService.GetByIdAsync(id);

            if (existingEntity == null)
                return NotFound("Registro não encontrado");

            var entityUpdated = await _productService.UpdateAsync(entity, cancellationToken);
            return Ok(entityUpdated);
        }

        return BadRequest(ModelState);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var existingEntity = await _productService.GetByIdAsync(id);

        if (existingEntity == null)
            return NotFound();

        await _productService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
