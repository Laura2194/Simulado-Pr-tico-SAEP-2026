using ClinicaOdontologica.Models;
using Microsoft.EntityFrameworkCore;
namespace ClinicaOdontologica.Data;
public class ClinicaContext : DbContext
{
    public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) { }
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Procedimento> Procedimentos => Set<Procedimento>();
    public DbSet<Agendamento> Agendamentos => Set<Agendamento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e => {
            e.ToTable("usuarios");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.Login).HasColumnName("login");
            e.Property(x => x.SenhaHash).HasColumnName("senha_hash");
            e.HasIndex(x => x.Login).IsUnique();
        });
        modelBuilder.Entity<Paciente>(e => {
            e.ToTable("pacientes");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.CpfCriptografado).HasColumnName("cpf_criptografado");
            e.Property(x => x.Telefone).HasColumnName("telefone");
        });
        modelBuilder.Entity<Procedimento>(e => {
            e.ToTable("procedimentos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Nome).HasColumnName("nome");
            e.Property(x => x.Descricao).HasColumnName("descricao");
            e.Property(x => x.Valor).HasColumnName("valor").HasColumnType("decimal(10,2)");
        });
        modelBuilder.Entity<Agendamento>(e => {
            e.ToTable("agendamentos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.PacienteId).HasColumnName("paciente_id");
            e.Property(x => x.ProcedimentoId).HasColumnName("procedimento_id");
            e.Property(x => x.DataAgendamento).HasColumnName("data_agendamento");
            e.HasOne(x => x.Paciente).WithMany(x => x.Agendamentos)
                .HasForeignKey(x => x.PacienteId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Procedimento).WithMany(x => x.Agendamentos)
                .HasForeignKey(x => x.ProcedimentoId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
