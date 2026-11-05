using System.ComponentModel.DataAnnotations;
namespace ClinicaOdontologica.Models;
public class Agendamento
{
    public int Id { get; set; }
    [Required] public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
    [Required] public int ProcedimentoId { get; set; }
    public Procedimento? Procedimento { get; set; }
    [Required] public DateTime DataAgendamento { get; set; }
}
