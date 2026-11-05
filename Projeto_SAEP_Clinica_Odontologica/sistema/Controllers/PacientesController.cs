using ClinicaOdontologica.Data;
using ClinicaOdontologica.Models;
using ClinicaOdontologica.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ClinicaOdontologica.Controllers;
[Authorize]
public class PacientesController : Controller
{
    private readonly ClinicaContext _db; private readonly CpfCryptoService _crypto;
    public PacientesController(ClinicaContext db, CpfCryptoService crypto) { _db=db; _crypto=crypto; }
    public async Task<IActionResult> Index(string? termo)
    {
        var lista = _db.Pacientes.AsQueryable();
        if (!string.IsNullOrWhiteSpace(termo)) lista = lista.Where(p => p.Nome.Contains(termo) || p.Telefone.Contains(termo));
        ViewBag.Termo=termo; return View(await lista.OrderBy(p=>p.Nome).ToListAsync());
    }
    public IActionResult Criar() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(string nome, string cpf, string telefone)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(cpf) || string.IsNullOrWhiteSpace(telefone))
        { ViewBag.Erro="Preencha todos os campos."; return View(); }
        _db.Pacientes.Add(new Paciente { Nome=nome.Trim(), CpfCriptografado=_crypto.Encrypt(cpf), Telefone=telefone.Trim() });
        await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Editar(int id)
    {
        var p=await _db.Pacientes.FindAsync(id); if(p==null) return NotFound();
        ViewBag.Cpf=_crypto.Decrypt(p.CpfCriptografado); return View(p);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, string nome, string cpf, string telefone)
    {
        var p=await _db.Pacientes.FindAsync(id); if(p==null) return NotFound();
        if(string.IsNullOrWhiteSpace(nome)||string.IsNullOrWhiteSpace(cpf)||string.IsNullOrWhiteSpace(telefone))
        { ViewBag.Erro="Preencha todos os campos."; ViewBag.Cpf=cpf; return View(p); }
        p.Nome=nome.Trim(); p.CpfCriptografado=_crypto.Encrypt(cpf); p.Telefone=telefone.Trim();
        await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
}
