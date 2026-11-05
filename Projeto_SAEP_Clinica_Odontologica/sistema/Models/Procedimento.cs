using System.ComponentModel.DataAnnotations;
namespace ClinicaOdontologica.Models;
public class Procedimento
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Nome { get; set; } = "";
    [StringLength(300)] public string Descricao { get; set; } = "";
    [Range(0, 999999)] public decimal Valor { get; set; }
    public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
}
