using System.ComponentModel.DataAnnotations;
namespace ClinicaOdontologica.Models;
public class Usuario
{
    public int Id { get; set; }
    [Required, StringLength(80)] public string Nome { get; set; } = "";
    [Required, StringLength(50)] public string Login { get; set; } = "";
    [Required] public string SenhaHash { get; set; } = "";
}
