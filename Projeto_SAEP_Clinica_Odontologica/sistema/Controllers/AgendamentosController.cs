using ClinicaOdontologica.Data;
using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ClinicaOdontologica.Controllers;
[Authorize]
public class AgendamentosController : Controller
{
    private readonly ClinicaContext _db;
    public AgendamentosController(ClinicaContext db) { _db=db; }
    public async Task<IActionResult> Index()
    {
        var itens=await _db.Agendamentos.Include(x=>x.Paciente).Include(x=>x.Procedimento)
            .OrderBy(x=>x.DataAgendamento).ToListAsync();
        return View(itens);
    }
    public async Task<IActionResult> Criar()
    {
        await CarregarListas(); return View();
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(int pacienteId, int procedimentoId, DateTime dataAgendamento)
    {
        if(!await _db.Pacientes.AnyAsync(x=>x.Id==pacienteId) || !await _db.Procedimentos.AnyAsync(x=>x.Id==procedimentoId))
        { ViewBag.Erro="Selecione um paciente e um procedimento cadastrados."; await CarregarListas(); return View(); }
        _db.Agendamentos.Add(new Agendamento { PacienteId=pacienteId, ProcedimentoId=procedimentoId, DataAgendamento=dataAgendamento });
        await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Editar(int id)
    {
        var a=await _db.Agendamentos.FindAsync(id); if(a==null) return NotFound();
        return View(a);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, DateTime dataAgendamento)
    {
        var a=await _db.Agendamentos.FindAsync(id); if(a==null) return NotFound();
        a.DataAgendamento=dataAgendamento; await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    private async Task CarregarListas()
    { ViewBag.Pacientes=await _db.Pacientes.OrderBy(x=>x.Nome).ToListAsync(); ViewBag.Procedimentos=await _db.Procedimentos.OrderBy(x=>x.Nome).ToListAsync(); }
}
