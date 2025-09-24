using CleanCode.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanCode.WebUI.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        _logger.LogInformation("Accessed Home/Index");
        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation("Accessed Home/Privacy");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
