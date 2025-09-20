using CleanCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.WebUI.Controllers;

public class ProductsController(
    ILogger<ProductsController> logger,
    IProductService productService) : Controller
{
    private readonly ILogger<ProductsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Accessed Products Index");

        var entity = await _productService.GetAllAsync();
        return View(entity);
    }
}
