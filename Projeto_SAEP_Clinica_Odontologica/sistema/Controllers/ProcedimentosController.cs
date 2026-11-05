using ClinicaOdontologica.Data;
using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ClinicaOdontologica.Controllers;
[Authorize]
public class ProcedimentosController : Controller
{
    private readonly ClinicaContext _db;
    public ProcedimentosController(ClinicaContext db) { _db=db; }
    public async Task<IActionResult> Index() => View(await _db.Procedimentos.OrderBy(x=>x.Nome).ToListAsync());
    public IActionResult Criar() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(string nome, string descricao, decimal valor)
    {
        if(string.IsNullOrWhiteSpace(nome)||valor<0) { ViewBag.Erro="Informe um nome e um valor válido."; return View(); }
        _db.Procedimentos.Add(new Procedimento { Nome=nome.Trim(), Descricao=descricao?.Trim()??"", Valor=valor });
        await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
}
