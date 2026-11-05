using System.ComponentModel.DataAnnotations;
namespace ClinicaOdontologica.Models;
public class Paciente
{
    public int Id { get; set; }
    [Required, StringLength(120)] public string Nome { get; set; } = "";
    [Required, StringLength(300)] public string CpfCriptografado { get; set; } = "";
    [Required, StringLength(20)] public string Telefone { get; set; } = "";
    public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
}
