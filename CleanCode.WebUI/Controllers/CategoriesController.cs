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
}
