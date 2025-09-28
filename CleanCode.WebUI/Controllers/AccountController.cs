using CleanCode.Domain.Account;
using CleanCode.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.WebUI.Controllers;

public class AccountController(
    ILogger<AccountController> logger,
    IAuthenticate authenticate) : Controller
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly IAuthenticate _authenticate = authenticate;

    [HttpGet]
    public IActionResult Index(string returnUrl)
    {
        return View(new LoginViewModel() 
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        try
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _authenticate.Authenticate(model.Email!, model.Password!);

                if (result)
                {
                    _logger.LogInformation("User logged in successfully: {Email}", model.Email);

                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                
                _logger.LogWarning("Invalid login attempt for user: {Email}", model.Email);
            }

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user: {Email}", model.Email);
            
            ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");
            
            return View();
        }
    }

    [HttpGet]
    public IActionResult Register() 
    { 
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticate.RegisterUser(model.Email!, model.Password!);
                
                if (result)
                {
                    _logger.LogInformation("User registered successfully: {Email}", model.Email);

                    return RedirectToAction("/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");

                    _logger.LogWarning("Registration failed for user: {Email}", model.Email);

                    return View(model);
                }
            }

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for user: {Email}", model.Email);

            ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");

            return View();
        }
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _authenticate.Logout();
        return RedirectToAction("/Account/Login");
    }
}
