using System.Security.Claims;
using ClinicaOdontologica.Data;
using ClinicaOdontologica.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaOdontologica.Controllers;
public class AccountController : Controller
{
    private readonly ClinicaContext _db; private readonly PasswordService _passwords;
    public AccountController(ClinicaContext db, PasswordService passwords) { _db=db; _passwords=passwords; }
    [HttpGet] public IActionResult Login() => User.Identity?.IsAuthenticated == true ? RedirectToAction("Index","Home") : View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string login, string senha)
    {
        var usuario = await _db.Usuarios.SingleOrDefaultAsync(x => x.Login == login);
        if (usuario == null || !_passwords.Verify(senha ?? "", usuario.SenhaHash))
        { ViewBag.Erro = "Usuário ou senha inválidos."; return View(); }
        var claims = new List<Claim> { new(ClaimTypes.Name, usuario.Nome), new(ClaimTypes.NameIdentifier, usuario.Id.ToString()) };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent=false, ExpiresUtc=DateTimeOffset.UtcNow.AddMinutes(20) });
        return RedirectToAction("Index","Home");
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Sair()
    { await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); return RedirectToAction("Login"); }
}
