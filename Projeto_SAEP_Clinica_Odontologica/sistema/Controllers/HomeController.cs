using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ClinicaOdontologica.Controllers;
[Authorize]
public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Erro() => View();
}
