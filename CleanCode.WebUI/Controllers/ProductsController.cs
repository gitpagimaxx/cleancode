using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanCode.WebUI.Controllers;

public class ProductsController(
    ILogger<ProductsController> logger,
    IProductService productService,
    ICategoryService categoryService,
    IWebHostEnvironment environment) : Controller
{
    private readonly ILogger<ProductsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IProductService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IWebHostEnvironment _environment = environment;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Accessed Products Index");

        var entity = await _productService.GetAllAsync();
        return View(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto entity)
    {
        if (ModelState.IsValid)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            await _productService.AddAsync(entity, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        return View(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return BadRequest();

        var entity = await _productService.GetByIdAsync(id.Value);

        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", entity!.CategoryId);

        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDto entity)
    {
        if (ModelState.IsValid)
        {
            try
            {
                CancellationToken cancellationToken = CancellationToken.None;
                await _productService.UpdateAsync(entity, cancellationToken);
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

        var entity = await _productService.GetByIdAsync(id.Value);

        if (entity == null)
            return NotFound();

        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, $"images\\{entity.Image}");
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return BadRequest();

        var entity = await _productService.GetByIdAsync(id.Value);

        if (entity == null)
            return NotFound();

        return View(entity);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        CancellationToken cancellationToken = CancellationToken.None;
        await _productService.DeleteAsync(id, cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}
