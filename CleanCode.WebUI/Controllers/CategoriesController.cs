using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.WebUI.Controllers;

public class CategoriesController(
    ILogger<CategoriesController> logger,
    ICategoryService categoryService) : Controller
{
    private readonly ILogger<CategoriesController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Accessed Categories Index");

        var entity = await _categoryService.GetAllAsync();
        return View(entity);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto entity)
    {
        if (ModelState.IsValid)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            await _categoryService.AddAsync(entity, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        return View(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return BadRequest();

        var entity = await _categoryService.GetByIdAsync(id.Value);
        
        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto entity)
    {
        if (ModelState.IsValid)
        {
            try
            {
                CancellationToken cancellationToken = CancellationToken.None;
                await _categoryService.UpdateAsync(entity, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id) 
    {
        if (id == null)
            return BadRequest();

        var entity = await _categoryService.GetByIdAsync(id.Value);

        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Delete (int? id)
    {
        if (id == null)
            return BadRequest();

        var entity = await _categoryService.GetByIdAsync(id.Value);

        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        CancellationToken cancellationToken = CancellationToken.None;
        await _categoryService.DeleteAsync(id, cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}
