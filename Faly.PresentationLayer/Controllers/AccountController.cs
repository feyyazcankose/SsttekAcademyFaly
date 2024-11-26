using System.Security.Claims;
using Faly.Core.Dtos.Ecommerce;
using Faly.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var client = _httpClientFactory.CreateClient("ApiClient");

        var response = await client.PostAsJsonAsync("api/users/login", model);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

            // JWT tokenını Claims içine ekleme
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.Email),
                new Claim("AccessToken", result.AccessToken),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("", "Giriş başarısız.");
            return View(model);
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
        if (!ModelState.IsValid)
            return View(model);

        var client = _httpClientFactory.CreateClient("ApiClient");

        var response = await client.PostAsJsonAsync("api/users/register", model);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Login");
        }
        else
        {
            ModelState.AddModelError("", "Kayıt başarısız.");
            return View(model);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        // Kullanıcının oturum bilgilerini al
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Login");
        }

        // Kullanıcının JWT'sini oturumdan al
        var token = User.FindFirst("AccessToken")?.Value;
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login");
        }

        // API'den kullanıcı bilgilerini çek
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"api/users/profile");

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<ProfileViewModel>();
            return View(user);
        }

        ModelState.AddModelError("", "Kullanıcı bilgileri alınamadı.");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Kullanıcının JWT'sini oturumdan al
        var token = User.FindFirst("AccessToken")?.Value;
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login");
        }

        // Kullanıcı bilgilerini API'ye gönder ve güncelle
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync("api/users/profile", model);

        if (response.IsSuccessStatusCode)
        {
            ViewBag.Message = "Profile updated successfully.";
        }
        else
        {
            ModelState.AddModelError("", "Profile updated error.");
        }

        return View(model);
    }
}
