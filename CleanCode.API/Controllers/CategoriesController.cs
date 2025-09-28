using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService) : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger = logger;
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogInformation("Accessed Categories GetAllAsync");
        var entities = await _categoryService.GetAllAsync();

        if (entities == null)
            return NotFound("Categories not found");

        return Ok(entities);
    }

    [HttpGet("{id:int}", Name = "GetByIdAsync")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var entity = await _categoryService.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();  
        }
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] CategoryDto entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            return BadRequest("Entity is null");
        
        var result = await _categoryService.AddAsync(entity, cancellationToken);
        return new CreatedAtRouteResult(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryDto entity, CancellationToken cancellationToken)
    {
        if (id != entity.Id)
            return BadRequest("ID mismatch");

        if (entity == null)
            return BadRequest("Os dados não foram enviados");

        if (ModelState.IsValid)
        {
            var existingEntity = await _categoryService.GetByIdAsync(id);

            if (existingEntity == null)
                return NotFound("Registro não encontrado");

            var entityUpdated = await _categoryService.UpdateAsync(entity, cancellationToken);
            return Ok(entityUpdated);
        }
        return BadRequest(ModelState);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var existingEntity = await _categoryService.GetByIdAsync(id);

        if (existingEntity == null)
            return NotFound();

        await _categoryService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
